using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblLogout_Click(object sender, EventArgs e)
        {
            //ToDo add logic to log out here
            Response.Redirect("../DefaultPage.aspx");
        }
    }
}