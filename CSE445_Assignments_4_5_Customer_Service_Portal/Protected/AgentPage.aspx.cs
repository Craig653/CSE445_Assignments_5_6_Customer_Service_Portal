using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Drawing.Imaging;
using System.Xml.Linq;
using System.ServiceModel;
using System.Text.Json;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class AgentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Craig's Xml Ticket loading
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
            doc.Load(path);

            //Open Tickets
            string xpath = "/Tickets/Ticket/Status[text()=\"Open\"]";
            var myNode = doc.SelectNodes(xpath);
            lblOpenTickets.Text = myNode.Count.ToString();

            //Closed Tickets
            xpath = "/Tickets/Ticket/Status[text()=\"Closed\"]";
            myNode = doc.SelectNodes(xpath);
            lblClosedTickets.Text = myNode.Count.ToString();

            //InProgress Tickets
            xpath = "/Tickets/Ticket/Status[text()=\"InProgress\"]";
            myNode = doc.SelectNodes(xpath);
            lbProgressTickets.Text = myNode.Count.ToString();


            //Craig's Code to force a Tree Reload
            TreeView1.DataSourceID = "";
            TreeView1.DataSourceID = "XmlDataSource1";

            xpath = "/Tickets/Ticket";
            XmlDataSource1.XPath = xpath;
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

        //Craig's Ticket Loading
        protected void lblLoadTicket_Click(object sender, EventArgs e)
        {

            lblTicketNumber.Text = "";
            lblRequester.Text = "";
            lblDescription.Text = "";
            lblCurrentStatus.Text = "";
            imgTicketImg.ImageUrl = "";
            lblTicketToolStatus.Text = "";
            lblAIResp.Text = "";
            lblStatusUpdate.Text = "";
            btnAnzImg.Enabled = false;
            btnAnzDesc.Enabled = false;
            btnSetOpen.Enabled = false;
            btnSetClosed.Enabled = false;
            btnSetInProgress.Enabled = false;

            if (txtLoadTicket.Text != "")
            {
                XmlDocument doc = new XmlDocument();
                string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
                doc.Load(path);
                string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
                var myNode = doc.SelectSingleNode(xpath);

                if (myNode != null) {
                    lblTicketNumber.Text = myNode.InnerText;
                    lblRequester.Text = myNode.ParentNode.ChildNodes[1].InnerText;
                    lblDescription.Text = myNode.ParentNode.ChildNodes[2].InnerText;
                    lblCurrentStatus.Text = myNode.ParentNode.ChildNodes[4].InnerText;
                    imgTicketImg.ImageUrl = "data:image/jpg;base64," + myNode.ParentNode.ChildNodes[3].InnerText;
                    lblTicketToolStatus.Text = "A Ticket has been found in the database";
                    btnAnzImg.Enabled = true;
                    btnAnzDesc.Enabled = true;
                    btnSetOpen.Enabled = true;
                    btnSetClosed.Enabled = true;
                    btnSetInProgress.Enabled = true;
                }
                else
                {
                    lblTicketToolStatus.Text = "Error Ticket Does not Exist!";
                }
            }
        }



        //Craigs Analyze the Ticket Description using GROQ AI
       
        protected void btnAnzDesc_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
            doc.Load(path);
            string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
            var myNode = doc.SelectSingleNode(xpath);

            string text = myNode.ParentNode.ChildNodes[2].InnerText;
            byte[] abc;
            Uri baseUri = new Uri("http://localhost:63092/Service1.svc");
            UriTemplate myTempalte = new UriTemplate("AskGroq/{text}");
            Uri completeUri = myTempalte.BindByPosition(baseUri, text);
            WebClient channel = new WebClient();
            try
            {
                abc = channel.DownloadData(completeUri);
            }
            catch (WebException error)
            {
                //Groq Error catching https://console.groq.com/docs/errors
                string errorStr = error.ToString();
                if (errorStr.Contains("(400)"))
                {
                    lblAIResp.Text = "The server could not understand the request due to invalid syntax. Review the request format and ensure it is correct.";
                }
                else if (errorStr.Contains("(401)"))
                {
                    lblAIResp.Text = "The request was not successful because it lacks valid authentication credentials for the requested resource. Ensure the request includes the necessary authentication credentials and the api key is valid.";
                }
                else if (errorStr.Contains("(404)"))
                {
                    lblAIResp.Text = "The requested resource could not be found. Check the request URL and the existence of the resource.";
                }
                else if (errorStr.Contains("(422)"))
                {
                    lblAIResp.Text = "The request was well-formed but could not be followed due to semantic errors. Verify the data provided for correctness and completeness.";
                }
                else if (errorStr.Contains("(429)"))
                {
                    lblAIResp.Text = "Too many requests were sent in a given timeframe. Implement request throttling and respect rate limits.";
                }

                lblAIResp.Visible = true;
                return;
            }

            Stream strm = new MemoryStream(abc);
            DataContractSerializer obj = new DataContractSerializer(typeof(string));
            string filteredText = obj.ReadObject(strm).ToString();

            Root myParsedConent = JsonSerializer.Deserialize<Root>(filteredText);


            lblAIResp.Text = myParsedConent.choices[0].message.content;
            lblAIResp.Visible = true;

        }


        //Craig's analyze image through Groq AI
        protected void btnAnzImg_Click(object sender, EventArgs e)
        {

            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
            doc.Load(path);
            string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
            var myNode = doc.SelectSingleNode(xpath);

            ImgGroq.Service1Client proxy = new ImgGroq.Service1Client();

            string jsonStr;

            try
            {
                jsonStr = proxy.ImgGroq(myNode.ParentNode.ChildNodes[3].InnerText);
            }

            catch (ProtocolException error)
            {
                //Groq Error catching https://console.groq.com/docs/errors
                string errorStr = error.ToString();
                if (errorStr.Contains("(400)"))
                {
                    lblAIResp.Text = "The server could not understand the request due to invalid syntax. Review the request format and ensure it is correct.";
                }
                else if (errorStr.Contains("(401)"))
                {
                    lblAIResp.Text = "The request was not successful because it lacks valid authentication credentials for the requested resource. Ensure the request includes the necessary authentication credentials and the api key is valid.";
                }
                else if (errorStr.Contains("(404)"))
                {
                    lblAIResp.Text = "The requested resource could not be found. Check the request URL and the existence of the resource.";
                }
                else if (errorStr.Contains("(413)"))
                {
                    lblAIResp.Text = "The remote server returned an unexpected response: (413) Request Entity Too Large. Files smaller than <25kb only!";
                }
                else if (errorStr.Contains("(422)"))
                {
                    lblAIResp.Text = "The request was well-formed but could not be followed due to semantic errors. Verify the data provided for correctness and completeness.";
                }
                else if (errorStr.Contains("(429)"))
                {
                    lblAIResp.Text = "Too many requests were sent in a given timeframe. Implement request throttling and respect rate limits.";
                }
                else
                {
                    lblAIResp.Text = "Something went wrong, try again.";
                }

                lblAIResp.Visible = true;
                return;
            }

            Root myParsedConent = JsonSerializer.Deserialize<Root>(jsonStr);

            lblAIResp.Text = myParsedConent.choices[0].message.content;
            lblAIResp.Visible = true;
        }


        //Craig's Status changing code
        protected void btnSetOpen_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
            doc.Load(path);
            string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
            var myNode = doc.SelectSingleNode(xpath);

            myNode.ParentNode.ChildNodes[4].InnerText = "Open";
            lblCurrentStatus.Text = "Open";
            doc.Save(path);
            lblStatusUpdate.Text = "Status set to Open";
            this.Page_Load(null, null);
        }


        //Craig's Status changing code
        protected void btnSetClosed_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
            doc.Load(path);
            string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
            var myNode = doc.SelectSingleNode(xpath);

            myNode.ParentNode.ChildNodes[4].InnerText = "Closed";
            lblCurrentStatus.Text = "Closed";
            doc.Save(path);

            lblStatusUpdate.Text = "Status set to Closed";
            this.Page_Load(null, null);
        }


        //Craig's Status changing code
        protected void btnSetInProgress_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
            doc.Load(path);
            string xpath = "/Tickets/Ticket/TicketNumber[text()=" + txtLoadTicket.Text + "]";
            var myNode = doc.SelectSingleNode(xpath);

            myNode.ParentNode.ChildNodes[4].InnerText = "InProgress";
            lblCurrentStatus.Text = "InProgress";
            doc.Save(path);

            lblStatusUpdate.Text = "Status set to InProgress";
            this.Page_Load(null, null);
        }
        protected void lblLogout_Click(object sender, EventArgs e)
        {
            //Todo add logic to logout here
            Response.Redirect("../DefaultPage.aspx");
        }

    }
    
}