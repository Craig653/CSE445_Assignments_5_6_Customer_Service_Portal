using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
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
            if (myAuthenticate(txtbxUsername.Text, txtbxPassword.Text))
                FormsAuthentication.RedirectFromLoginPage(txtbxUsername.Text, true);
                //Response.Redirect("Protected/MemberPage.aspx");
            else
                lblAuthentication.Text = "Invalid login";
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

            return true;
        }
    }
}