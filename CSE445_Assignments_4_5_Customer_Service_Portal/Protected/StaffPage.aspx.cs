using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadStaffAccounts();
            loadAgentAccounts();
            loadMemberAccounts();
            
        }
        protected void btnLoginStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffPage.aspx");
        }

        protected void btnLoginMember_Click(object sender, EventArgs e)
        {
            //check login cookies and database
            //Server.Transfer("LoginPage.aspx");
            Response.Redirect("MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {
            //check login cookies and database
            //Server.Transfer("LoginPage.aspx");
            Response.Redirect("AgentPage.aspx");
        }

        protected void btnTryIt_Click(object sender, EventArgs e)
        {
            Response.Redirect("../TryItPage.aspx");
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("../DefaultPage.aspx");
        }
        protected void btnComponentTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ComponentTable.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //Craig's Get username cookie on Load
            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
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
                Server.Transfer("../DefaultPage.aspx");
            }
        }

        protected void loadStaffAccounts()
        {
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/Staff.xml");
            doc.Load(path);
            string label = "";

            foreach(XmlNode node in doc)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    label += child.FirstChild.FirstChild.Value + "<br/> ";

                }

            }
            lblStaffList.Text = label;

        }

        protected void loadAgentAccounts()
        {
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/Agent.xml");
            doc.Load(path);
            string label = "";

            foreach (XmlNode node in doc)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    label += child.FirstChild.FirstChild.Value + "<br/> ";

                }

            }

            lblAgentList.Text = label;

        }

        protected void loadMemberAccounts()
        {
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/Member.xml");
            doc.Load(path);
            string label = "";

            foreach (XmlNode node in doc)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    label += child.FirstChild.FirstChild.Value + "<br/> ";

                }

            }

            lblMemberList.Text = label;
        }
    }
}