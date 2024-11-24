﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
    }
}