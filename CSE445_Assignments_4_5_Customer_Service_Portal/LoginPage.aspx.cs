using LocalHash;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        private string buttonID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            // Retrieve the button ID from the session
            buttonID = Session["buttonID"] as string;
            Debug.WriteLine("Button ID from session: " + buttonID);
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
            string userType = "";
            Debug.WriteLine("btnLogin_Click event triggered"); // for debugging purposes
            Button clickedButton = sender as Button; // get the button object that was clicked
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
                    userType = "Admin";
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

            if (myAuthenticate(txtbxUsername.Text, txtbxPassword.Text, userType)) // if the user is authenticated successfully
            {
                FormsAuthentication.RedirectFromLoginPage(txtbxUsername.Text, true);
                //Response.Redirect("Protected/MemberPage.aspx");
            }
            else
            {
                lblAuthentication.Text = "Invalid login";
            }
        }

        // helper for encrypting passwords
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt(); // create a new CredentialEncrypt object
            return encryptor.EncryptString(password); // return the encrypted password string
        }

        // helper function for authenticating users
        protected bool myAuthenticate(string username, string password, string userType)
        {
            Debug.WriteLine("Authentication Starting"); // for debugging purposes

            string memberFile = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Member.xml"); // Member xml file path
            string staffFile = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Staff.xml");  // Staff xml file path
            string adminFile = Path.Combine(Request.PhysicalApplicationPath, @"App_Data\Admin.xml"); // Admin xml file path

            if (File.Exists(memberFile) && File.Exists(staffFile) && File.Exists(adminFile)) // verify all xml credential files exist
            {
                Debug.WriteLine("All XML Credentials File Found"); // for debugging purposes

                if (userType.Equals("Member")) // if the user type is Member
                {
                    using (FileStream FS = new FileStream(memberFile, FileMode.Open, FileAccess.Read, FileShare.Read)) // open xml file
                    {
                        Debug.WriteLine("FileStream for XML Opened"); // for debugging purposes
                        XmlDocument xd = new XmlDocument(); // create a new XmlDocument object
                        xd.Load(FS); // load the xml file
                        XmlNodeList users = xd.SelectNodes("/Member/Credentials"); // select the Member credentials

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
                            if (xmlUsername.Equals(username) && xmlUserType.Equals(userType))
                            {
                                Debug.WriteLine("Username and UserType Matched"); // for debugging purposes
                                string encryptedPassword = EncryptPassword(password);
                                Debug.WriteLine("Encrypted Given Password: " + encryptedPassword);
                                if (xmlPassword.Equals(encryptedPassword))
                                {
                                    Debug.WriteLine("Password Matched");
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
                else if (userType.Equals("Staff"))
                {
                    using (FileStream FS = new FileStream(staffFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        Debug.WriteLine("FileStream for XML Opened");
                        XmlDocument xd = new XmlDocument();
                        xd.Load(FS);
                        XmlNodeList users = xd.SelectNodes("/Staff/Credentials");

                        foreach (XmlNode user in users)
                        {
                            string xmlUsername = user["Username"].InnerText;
                            string xmlPassword = user["Password"].InnerText;
                            string xmlUserType = user.Attributes["UserType"].Value;

                            Debug.WriteLine("XML UserType: " + xmlUserType);
                            Debug.WriteLine("XML Username: " + xmlUsername);
                            Debug.WriteLine("XML Password: " + xmlPassword);

                            if (xmlUsername.Equals(username) && xmlUserType.Equals(userType))
                            {
                                Debug.WriteLine("Username and UserType Matched");
                                string encryptedPassword = EncryptPassword(password);
                                Debug.WriteLine("Encrypted Given Password: " + encryptedPassword);
                                if (xmlPassword.Equals(encryptedPassword))
                                {
                                    Debug.WriteLine("Password Matched");
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
                else if (userType.Equals("AdminStaff"))
                {
                    using (FileStream FS = new FileStream(adminFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        Debug.WriteLine("FileStream for XML Opened");
                        XmlDocument xd = new XmlDocument();
                        xd.Load(FS);
                        XmlNodeList users = xd.SelectNodes("/Admin/Credentials");

                        foreach (XmlNode user in users)
                        {
                            string xmlUsername = user["Username"].InnerText;
                            string xmlPassword = user["Password"].InnerText;
                            string xmlUserType = user.Attributes["UserType"].Value;

                            Debug.WriteLine("XML UserType: " + xmlUserType);
                            Debug.WriteLine("XML Username: " + xmlUsername);
                            Debug.WriteLine("XML Password: " + xmlPassword);

                            if (xmlUsername.Equals(username) && xmlUserType.Equals(userType))
                            {
                                Debug.WriteLine("Username and UserType Matched");
                                string encryptedPassword = EncryptPassword(password);
                                Debug.WriteLine("Encrypted Given Password: " + encryptedPassword);
                                if (xmlPassword.Equals(encryptedPassword))
                                {
                                    Debug.WriteLine("Password Matched");
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                } else
                {
                    return false;
                }
            }
            return false;
        }

    }
}