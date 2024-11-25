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

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
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
            if (myAuthenticate(txtbxUsername.Value, txtbxPassword.Value))
                FormsAuthentication.RedirectFromLoginPage(txtbxUsername.Value, true);
                //Response.Redirect("Protected/MemberPage.aspx");
            else
                lblAuthentication.Text = "Invalid Login, talk to a Staff member to get access to that page";
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

            HttpCookie mycookies = Request.Cookies["Username"];
            HttpCookie mycookiesType = Request.Cookies["Type"];

            if (txtbxUsername.Value.Length > 0)
            {
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


            }

            return true;
        }
    }
}