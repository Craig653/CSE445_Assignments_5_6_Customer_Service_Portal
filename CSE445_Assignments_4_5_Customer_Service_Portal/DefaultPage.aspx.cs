using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Check session state to set login button
            if (Session["Username"] != null)
            {
                Login.InnerText = "Logout";
            }
            else
            {
                Login.InnerText = "Login";
            }
        }

        //page redirect functions
        protected void btnLoginStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("Protected/StaffPage.aspx");
        }

        protected void btnLoginMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("Protected/MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {
            Response.Redirect("Protected/AgentPage.aspx");
        }

        protected void btnTryIt_Click(object sender, EventArgs e)
        {
            Response.Redirect("TryItPage.aspx");
        }
        protected void btnComponentTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("ComponentTable.aspx");
        }
        protected void btnDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("DefaultPage.aspx");
        }


        //Logout and delete all session and cookies
        protected void btnLoginOut_Click(object sender, EventArgs e)
        {
            //Craig's Get username cookie on Load
            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
                Session["Username"] = null;
                Session["AccountType"] = null;
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
                Server.Transfer("DefaultPage.aspx");
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }


    }
}