using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.XmlDataSource;
using System.Xml;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = "";
            HttpCookie userCookie = Request.Cookies["Username"];
            if (userCookie != null)
            {
                username = userCookie.Value.ToString();
                if (username.Contains('='))
                {
                    username = username.Split('=')[1];
                }
                else
                {
                    // Handle the case where the username does not contain '='
                    username = ""; // Or set to a default value or handle the error appropriately
                }
            }
            string xpath = "//Tickets/Ticket[RequestingUsername[text()=\"" + username + "\"]]";
            //XmlDataSource1.XPath = xpath;
        }

        protected void lblLogout_Click(object sender, EventArgs e)
        {
            //ToDo add logic to log out here
            Response.Redirect("../DefaultPage.aspx");
        }
    }
}