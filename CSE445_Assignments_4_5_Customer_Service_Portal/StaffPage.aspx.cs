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

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class StaffPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblLoadTicket_Click(object sender, EventArgs e)
        {

            lblTicketNumber.Text = "";
            lblRequester.Text = "";
            lblDescription.Text = "";
            lblCurrentStatus.Text = "";
            imgTicketImg.ImageUrl = "";
            lblTicketToolStatus.Text = "";
            lblAIResp.Text = "";
            btnAnzImg.Enabled = false;
            btnAnzDesc.Enabled = false;

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
                }
                else
                {
                    lblTicketToolStatus.Text = "Error Ticket Does not Exist!";
                }
            }
        }

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
    }
    
}