using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class StaffPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblLoadTicket_Click(object sender, EventArgs e)
        {


            if(txtLoadTicket.Text != "")
            {
                XmlDocument doc = new XmlDocument();
                string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
                doc.Load(path);
                string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
                var myNode = doc.SelectSingleNode(xpath);
                return;

            }



        }
    }
}