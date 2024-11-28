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

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Panel1.Visible)
            {
                Panel1.Visible = false;
            }
        }
        protected void btnLoginStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("Protected/StaffPage.aspx");
        }

        protected void btnLoginMember_Click(object sender, EventArgs e)
        {
            //check login cookies and database
            //Server.Transfer("LoginPage.aspx");
            Response.Redirect("Protected/MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {
            //check login cookies and database
            //Server.Transfer("LoginPage.aspx");
            Response.Redirect("Protected/AgentPage.aspx");
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtbxUsername.Value == "" && txtbxPassword.Value == "")
            {
                lblAuthentication.Text = "Please enter your username and password";
            }
            else if(txtbxUsername.Value == "")
            {
                lblAuthentication.Text = "Please enter a username";
            }
            else if ( txtbxPassword.Value == "")
            {
                lblAuthentication.Text = "Please enter a password";
            }
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

        //Look up account type in case cookies aren't enable
        protected string checkAccountType(string username)
        {

            XmlDocument docStaff = new XmlDocument();
            string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
            docStaff.Load(pathStaff);
            XmlNode rootStaff = docStaff.DocumentElement;
            string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + username + "\"" + "]";
            var myNodeStaff = docStaff.SelectSingleNode(xpath1);

            XmlDocument docAgent = new XmlDocument(); ;
            string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
            docAgent.Load(pathAgent);
            XmlNode rootAgent = docAgent.DocumentElement;
            string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + username + "\"" + "]";
            var myNodeAgent = docAgent.SelectSingleNode(xpath2);

            XmlDocument docMember = new XmlDocument();
            string pathMember = Server.MapPath("~/App_Data/Member.xml");
            docMember.Load(pathMember);
            XmlNode rootMember = docMember.DocumentElement;
            string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + username + "\"" + "]";
            var myNodeMember = docMember.SelectSingleNode(xpath3);

            if(myNodeStaff != null)
            {
                return "/Protected/StaffPage.aspx";
            }
            else if(myNodeAgent != null)
            {
                return "/Protected/AgentPage.aspx";
            }
            else if(myNodeMember != null)
            {
                return "/Protected/MemberPage.aspx";
            }
            else
            {
                return "ERROR";
            }

        }

        protected void btnLoginBar_Click(object sender, EventArgs e)
        {
            //do nothing here, don't need to go back to the login page haha 
        }


        protected bool myAuthenticate(string username, string password)
        {
            string fLocation = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Users.xml");
            if (File.Exists(fLocation))
            {
                FileStream FS = new FileStream(fLocation, FileMode.Open);
                XmlDocument xd = new XmlDocument();
                xd.Load(FS);
                XmlNode node = xd;
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode child in children)
                {
                    // use hash function if the credential is hashed
                    // check if the username and password exist in the XML file;

                }
            }


            //Todo get account type here
            string accountType = "Staff";



            //Need session state and cookies
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

            return true;
        }

        //Chris's DLL encryption
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt();
            return encryptor.EncryptString(password);
        }

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

                bool testCaptcha = Captcha1.validate();

                if (testCaptcha)
                {
                    //only can create member accounts
                    if ( myNodeMember == null)
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

                        docMember.Save(pathMember);

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