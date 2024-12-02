using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LocalHash;
using System.Runtime.Remoting.Lifetime;
using System.ServiceModel.Channels;
using System.Web.SessionState;
using System.Diagnostics;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            userLogout();

            //Only show buttons users have access to
            if (Session["AccountType"] != null)
            {
                if (Session["AccountType"] == "Staff")
                {
                    btnLoginStaff.Visible = true;
                }
                else if (Session["AccountType"] == "Agent")
                {
                    btnLoginAgent.Visible = true;
                }
                else if (Session["AccountType"] == "Member")
                {
                    btnMemberLogin.Visible = true;
                }
            }

            //don't show create account unless they ask for it
            if (!Panel1.Visible)
            {
                Panel1.Visible = false;
            }

            HttpCookie lastUsername = Request.Cookies["LastUsername"];
            HttpCookie lastPassword = Request.Cookies["LastPassword"];

            if (lastUsername != null && lastPassword != null)
            {

                btnCookieLogin.Visible = true;
                btnCookieLogin.InnerText = "Login with Cookies (" + lastUsername["LastUsername"].ToString() + ")";
            }
            else
            {
                btnCookieLogin.Visible = false;
            }
        }

        protected void btnLogin_cookies_Click(object sender, EventArgs e)
        {
            HttpCookie lastUsername = Request.Cookies["LastUsername"];
            HttpCookie lastPassword = Request.Cookies["LastPassword"];

            if (lastUsername != null && lastPassword != null)
            {
                string username = lastUsername["LastUsername"];
                string password = lastPassword["LastPassword"];

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    // Automatically populate the username and password fields
                    txtbxUsername.Value = username;
                    txtbxPassword.Value = password; // Populate the password field
                }
            }

            btnLogin_Click(null, null);
        }

        //Page redirect functions
        protected void btnLoginStaff_Click(object sender, EventArgs e)
        {
            Session["buttonID"] = "btnLoginStaff";
            Response.Redirect("Staff/StaffPage.aspx");
        }

        protected void btnLoginMember_Click(object sender, EventArgs e)
        {
            Session["buttonID"] = "btnLoginMember";
            Response.Redirect("Member/MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {
            Session["buttonID"] = "btnLoginAgent";
            Response.Redirect("Agent/AgentPage.aspx");
        }

        protected void btnTryIt_Click(object sender, EventArgs e)
        {
            Response.Redirect("TryItPage.aspx");
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("DefaultPage.aspx");
        }

        protected void btnShowCreate_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        private void userLogout()
        {
            //logout clean up session and cookies

            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
                Session["Username"] = null;
                Session["AccountType"] = null;
                Session["buttonID"] = null;
                HttpCookie delCookie = new HttpCookie("Username");
                delCookie.Expires = DateTime.Now.AddMonths(-10);
                delCookie.Value = null;
                Response.Cookies.Add(delCookie);
                HttpContext.Current.Request.Cookies.Clear();

                delCookie = new HttpCookie("Type");
                delCookie.Expires = DateTime.Now.AddMonths(-10);
                delCookie.Value = null;
                Response.Cookies.Add(delCookie);
                HttpContext.Current.Request.Cookies.Clear();

                FormsAuthentication.SignOut();
            }
        }

        //login clicking function, will authenticate and check if you typed everhting
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtbxUsername.Value == "" && txtbxPassword.Value == "")
            {
                lblAuthentication.Text = "Please enter your username and password";
            }
            else if (txtbxUsername.Value == "")
            {
                lblAuthentication.Text = "Please enter a username";
            }
            else if (txtbxPassword.Value == "")
            {
                lblAuthentication.Text = "Please enter a password";
            }
            //if you've typed everything log in
            else
            {
                if (myAuthenticate(txtbxUsername.Value, txtbxPassword.Value))
                {
                    FormsAuthentication.RedirectFromLoginPage(txtbxUsername.Value, true);
                }
                else
                {
                    lblAuthentication.Text = "Invalid Login Try Again";
                }
            }


        }

        protected void btnLoginBar_Click(object sender, EventArgs e)
        {
            //do nothing here, don't need to go back to the login page haha
        }


        private string getButtonType()
        {
            string buttonID = "";
            string userType = "";
            if (Session["buttonID"] != null)
            {
                buttonID = Session["buttonID"].ToString();
                if (!string.IsNullOrEmpty(buttonID)) // if buttonID exists (which means button was clicked)
                {
                    Debug.WriteLine("Button clicked: " + buttonID); // for debugging purposes
                    if (buttonID.Equals("btnLoginMember")) // if the button clicked was the Member login button
                    {
                        userType = "Member";
                        Debug.WriteLine(userType + " Login Detected");
                    }
                    else if (buttonID.Equals("btnLoginAgent")) // if the button clicked was the Agent login button
                    {
                        userType = "Agent";
                        Debug.WriteLine(userType + " Login Detected");
                    }
                    else if (buttonID.Equals("btnLoginStaff")) // if the button clicked was the Staff login button
                    {
                        userType = "Staff";
                        Debug.WriteLine(userType + " Login Detected");
                    }
                    else
                    {
                        userType = "Unknown"; // no good [no login button was clicked :( ]
                    }
                }
            }
            else
            {
                userType = "Unknown";
            }


            return userType;
        }

        protected bool myAuthenticate(string username, string password)
        {

            string accountType = getButtonType();

            //Chris's Authenticate
            Debug.WriteLine("Authentication Starting"); // for debugging purposes

            string memberFile = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Member.xml"); // Member xml file path
            string staffFile = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Staff.xml");  // Staff xml file path
            string adminFile = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Agent.xml"); // Admin xml file path

            if (File.Exists(memberFile) && File.Exists(staffFile) && File.Exists(adminFile)) // verify all xml credential files exist
            {
                Debug.WriteLine("All XML Credentials File Found"); // for debugging purposes

                if (accountType.Equals("Member")) // if the user type is Member
                {
                    using (FileStream FS = new FileStream(memberFile, FileMode.Open, FileAccess.Read, FileShare.Read)) // open xml file
                    {
                        Debug.WriteLine("FileStream for XML Opened"); // for debugging purposes
                        XmlDocument xd = new XmlDocument(); // create a new XmlDocument object
                        xd.Load(FS); // load the xml file
                        XmlNodeList users = xd.SelectNodes("/CredentialsDatabase/Credentials"); // select the Member credentials

                        foreach (XmlNode user in users) // iterate through the users in the xml file
                        {
                            string xmlUsername = user["Username"].InnerText; // get the username from the xml file
                            string xmlPassword = user["Password"].InnerText; // get the password from the xml file
                            string xmlUserType = user.Attributes["UserType"].Value; // get the user type from the xml file

                            Debug.WriteLine("XML UserType: " + xmlUserType); // for debugging purposes
                            Debug.WriteLine("XML Username: " + xmlUsername); // for debugging purposes
                            Debug.WriteLine("XML Password: " + xmlPassword); // for debugging purposes

                            // if user-provided username and password match the xml username and password
                            // password is necrypted in the xml file and user-given password is encrypted before comparison
                            if (xmlUsername.Equals(username) && xmlUserType.Equals(accountType))
                            {
                                Debug.WriteLine("Username and UserType Matched"); // for debugging purposes
                                string encryptedPassword = EncryptPassword(password);
                                Debug.WriteLine("Encrypted Given Password: " + encryptedPassword);
                                if (xmlPassword.Equals(encryptedPassword))
                                {
                                    Debug.WriteLine("Password Matched");
                                    setCookies();
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else if (accountType.Equals("Staff"))
                {
                    using (FileStream FS = new FileStream(staffFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        Debug.WriteLine("FileStream for XML Opened");
                        XmlDocument xd = new XmlDocument();
                        xd.Load(FS);
                        XmlNodeList users = xd.SelectNodes("/CredentialsDatabase/Credentials");

                        foreach (XmlNode user in users)
                        {
                            string xmlUsername = user["Username"].InnerText;
                            string xmlPassword = user["Password"].InnerText;
                            string xmlUserType = user.Attributes["UserType"].Value;

                            Debug.WriteLine("XML UserType: " + xmlUserType);
                            Debug.WriteLine("XML Username: " + xmlUsername);
                            Debug.WriteLine("XML Password: " + xmlPassword);

                            if (xmlUsername.Equals(username) && xmlUserType.Equals(accountType))
                            {
                                Debug.WriteLine("Username and UserType Matched");
                                string encryptedPassword = EncryptPassword(password);
                                Debug.WriteLine("Encrypted Given Password: " + encryptedPassword);
                                if (xmlPassword.Equals(encryptedPassword))
                                {
                                    Debug.WriteLine("Password Matched");
                                    setCookies();
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else if (accountType.Equals("Agent"))
                {
                    using (FileStream FS = new FileStream(adminFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        Debug.WriteLine("FileStream for XML Opened");
                        XmlDocument xd = new XmlDocument();
                        xd.Load(FS);
                        XmlNodeList users = xd.SelectNodes("/CredentialsDatabase/Credentials");

                        foreach (XmlNode user in users)
                        {
                            string xmlUsername = user["Username"].InnerText;
                            string xmlPassword = user["Password"].InnerText;
                            string xmlUserType = user.Attributes["UserType"].Value;

                            Debug.WriteLine("XML UserType: " + xmlUserType);
                            Debug.WriteLine("XML Username: " + xmlUsername);
                            Debug.WriteLine("XML Password: " + xmlPassword);

                            if (xmlUsername.Equals(username) && xmlUserType.Equals(accountType))
                            {
                                Debug.WriteLine("Username and UserType Matched");
                                string encryptedPassword = EncryptPassword(password);
                                Debug.WriteLine("Encrypted Given Password: " + encryptedPassword);
                                if (xmlPassword.Equals(encryptedPassword))
                                {
                                    Debug.WriteLine("Password Matched");
                                    setCookies();
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //scrap xmls for Accoutn type for generic login
                    XmlDocument docStaff = new XmlDocument();
                    string path = Server.MapPath("~/App_Data/Staff.xml");
                    docStaff.Load(path);
                    string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername.Value + "\"" + "]";
                    var myNodeStaff = docStaff.SelectSingleNode(xpath1);

                    XmlDocument docAgent = new XmlDocument(); ;
                    string path2 = Server.MapPath("~/App_Data/Agent.xml");
                    docAgent.Load(path2);
                    string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername.Value + "\"" + "]";
                    var myNodeAgent = docAgent.SelectSingleNode(xpath2);

                    XmlDocument docMember = new XmlDocument();
                    string path3 = Server.MapPath("~/App_Data/Member.xml");
                    docMember.Load(path3);
                    string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername.Value + "\"" + "]";
                    var myNodeMember = docMember.SelectSingleNode(xpath3);

                    if (myNodeStaff != null)
                    {
                        Session["buttonID"] = "btnLoginStaff";
                        return myAuthenticate(username, password);
                    }
                    else if (myNodeAgent != null)
                    {
                        Session["buttonID"] = "btnLoginAgent";
                        return myAuthenticate(username, password);
                    }
                    else if (myNodeMember != null)
                    {
                        Session["buttonID"] = "btnLoginMember";
                        return myAuthenticate(username, password);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        private void setCookies()
        {

            string accountType = getButtonType();
            //Need session state and cookies to be setup for other functions
            HttpCookie mycookies = Request.Cookies["Username"];
            HttpCookie mycookiesType = Request.Cookies["Type"];

            if (txtbxUsername.Value.Length > 0)
            {
                //set cookies
                if ((mycookies == null) || mycookies["Username"] == "")
                {
                    HttpCookie newCookies = new HttpCookie("Username");
                    newCookies["Username"] = txtbxUsername.Value;
                    newCookies.Expires = DateTime.Now.AddMonths(6);
                    Response.Cookies.Add(newCookies);
                }
                else
                {
                    mycookies["Username"] = txtbxUsername.Value;
                    Response.Cookies.Add(mycookies);
                }

                if ((mycookiesType == null) || mycookiesType["Type"] == "")
                {
                    HttpCookie newType = new HttpCookie("Type");
                    newType["Type"] = accountType;
                    newType.Expires = DateTime.Now.AddMonths(6);
                    Response.Cookies.Add(newType);
                }
                else
                {
                    mycookiesType["Type"] = accountType;
                    Response.Cookies.Add(mycookiesType);
                }

                //set session state
                Session["Username"] = txtbxUsername.Value;
                Session["AccountType"] = accountType;
            }


            HttpCookie lastUsername = Request.Cookies["LastUsername"];
            HttpCookie lastPassword = Request.Cookies["LastPassword"];

            if(lastUsername == null)
            {
                HttpCookie lastUsernames = new HttpCookie("LastUsername");
                lastUsernames["LastUsername"] = txtbxUsername.Value;
                lastUsernames.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(lastUsernames);
            }
            else
            {
                lastUsername["LastUsername"] = txtbxUsername.Value;
                Response.Cookies.Add(lastUsername);
            }

            if (lastPassword == null)
            {
                HttpCookie lastPasswords = new HttpCookie("LastPassword");
                lastPasswords["LastPassword"] = txtbxPassword.Value;
                lastPasswords.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(lastPasswords);
            }
            else
            {
                lastPassword["LastPassword"] = txtbxPassword.Value;
                Response.Cookies.Add(lastPassword);
            }
        }


        //Chris's DLL encryption
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt();
            return encryptor.EncryptString(password);
        }


        //Create new account
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Password1.Value == "" && txtbxUsername1.Value == "")
            {
                lblCreateStatus.Text = "Enter a Username and Password";
            }
            else if (txtbxUsername1.Value == "")
            {
                lblCreateStatus.Text = "Enter a Username";
            }
            else if (Password1.Value == "")
            {
                lblCreateStatus.Text = "Enter a Password";
            }
            else
            {
                //load all XMLS needed
                XmlDocument docStaff = new XmlDocument();
                string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
                docStaff.Load(pathStaff);
                XmlNode rootStaff = docStaff.DocumentElement;
                string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername1.Value + "\"" + "]";
                var myNodeStaff = docStaff.SelectSingleNode(xpath1);

                XmlDocument docAgent = new XmlDocument(); ;
                string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
                docAgent.Load(pathAgent);
                XmlNode rootAgent = docAgent.DocumentElement;
                string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername1.Value + "\"" + "]";
                var myNodeAgent = docAgent.SelectSingleNode(xpath2);

                XmlDocument docMember = new XmlDocument();
                string pathMember = Server.MapPath("~/App_Data/Member.xml");
                docMember.Load(pathMember);
                XmlNode rootMember = docMember.DocumentElement;
                string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername1.Value + "\"" + "]";
                var myNodeMember = docMember.SelectSingleNode(xpath3);

                XmlDocument docMemberWeb = new XmlDocument();
                string pathMemberWeb = Server.MapPath("~/Member/web.config");
                docMemberWeb.Load(pathMemberWeb);
                string xpath4 = "/configuration/system.web/authorization";
                string xpath5 = "/configuration/system.web/authorization/deny";
                var myNodeMemberWeb = docMemberWeb.SelectSingleNode(xpath4);
                var myNodeMemberWebRef = docMemberWeb.SelectSingleNode(xpath5);


                bool testCaptcha = Captcha1.validate();


                //Check captacha
                if (testCaptcha)
                {

                    //Check if account already exists
                    if (myNodeMember == null & myNodeAgent == null && myNodeStaff == null)
                    {
                        XmlElement Credentials = docMember.CreateElement("Credentials");
                        Credentials.SetAttribute("UserType", "Member");
                        XmlElement Username = docMember.CreateElement("Username");
                        Username.InnerText = txtbxUsername1.Value;
                        XmlElement Password = docMember.CreateElement("Password");
                        Password.InnerText = EncryptPassword(Password1.Value);

                        Credentials.AppendChild(Username);
                        Credentials.AppendChild(Password);

                        rootMember.AppendChild(Credentials);

                        //Same new member account
                        docMember.Save(pathMember);

                        //update webconfig to allow user as a member
                        XmlElement WebUser = docMemberWeb.CreateElement("allow");
                        WebUser.SetAttribute("users", txtbxUsername1.Value);
                        myNodeMemberWeb.PrependChild(WebUser);
                        docMemberWeb.Save(pathMemberWeb);

                        lblCreateStatus.Text = "Member Account: " + txtbxUsername1.Value + " created";
                    }

                    else
                    {
                        if (myNodeStaff != null)
                        {
                            lblCreateStatus.Text = "Staff Account: " + txtbxUsername1.Value + " already exists";
                        }
                        else if (myNodeAgent != null)
                        {
                            lblCreateStatus.Text = "Agent Account: " + txtbxUsername1.Value + " already exists";
                        }
                        else if (myNodeMember != null)
                        {
                            lblCreateStatus.Text = "Member Account: " + txtbxUsername1.Value + " already exists";
                        }
                    }
                }
            }
        }
    }
}