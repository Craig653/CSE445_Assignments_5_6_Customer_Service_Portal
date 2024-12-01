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
            //Only show buttons users have access to
            if (Session["AccountType"] != null)
            {
                if(Session["AccountType"] == "Staff")
                {
                    btnLoginStaff.Visible = true;
                }
                else if(Session["AccountType"] == "Agent")
                {
                    btnLoginAgent.Visible = true;
                }
                else if(Session["AccountType"] == "Member")
                {
                    btnMemberLogin.Visible = true;
                }
            }

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
                Server.Transfer("DefaultPage.aspx");
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void btnToMember_Click(object sender, EventArgs e)
        {
            Session["buttonID"] = "btnLoginMember";
            Response.Redirect("Member/MemberPage.aspx");
        }

        protected void btnToAgentPage_Click(object sender, EventArgs e)
        {
            Session["buttonID"] = "btnLoginAgent";
            Response.Redirect("Agent/AgentPage.aspx");
        }

        protected void btnToStaffPage_Click(object sender, EventArgs e)
        {
            Session["buttonID"] = "btnLoginStaff";
            Response.Redirect("Staff/StaffPage.aspx");
        }
    }
}