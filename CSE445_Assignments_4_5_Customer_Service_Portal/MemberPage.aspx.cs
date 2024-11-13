using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = "";
            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
                username = userCookie.Value.ToString();
                username = username.Split('=')[1];
            }

            string xpath = "//Tickets/Ticket[RequestingUsername[text()=\"" + username+ "\"]]";

            XmlDataSource1.XPath = xpath;
        }

        protected string getCookieElement()
        {
            string cookie = "Craig653";

            return cookie;
        }
        protected void btnSubmitTicket_Click(object sender, EventArgs e)
        {
            if(txtIssueBox.Text.Length > 0 && FileUpload2.HasFile)
            {
                string filename = FileUpload2.FileName;
                string img = "";

                string extention = Path.GetExtension(filename);
                if (extention != ".jpeg")
                {
                    lblSubmitStatus.Text = "Not a JPEG File Try again";
                    lblSubmitStatus.Visible = true;
                }
                else
                {
                    img = Convert.ToBase64String(FileUpload2.FileBytes);

                    ImgGroq.Service1Client proxy = new ImgGroq.Service1Client();

                    string jsonStr;

                    try
                    {
                        jsonStr = proxy.ImgGroq(img);
                    }

                    catch (ProtocolException error)
                    {
                        //Groq Error catching https://console.groq.com/docs/errors
                        string errorStr = error.ToString();
                        if (errorStr.Contains("(400)"))
                        {
                            lblSubmitStatus.Text = "The server could not understand the request due to invalid syntax. Review the request format and ensure it is correct.";
                        }
                        else if (errorStr.Contains("(401)"))
                        {
                            lblSubmitStatus.Text = "The request was not successful because it lacks valid authentication credentials for the requested resource. Ensure the request includes the necessary authentication credentials and the api key is valid.";
                        }
                        else if (errorStr.Contains("(404)"))
                        {
                            lblSubmitStatus.Text = "The requested resource could not be found. Check the request URL and the existence of the resource.";
                        }
                        else if (errorStr.Contains("(413)"))
                        {
                            lblSubmitStatus.Text = "The remote server returned an unexpected response: (413) Request Entity Too Large. Files smaller than <25kb only!";
                        }
                        else if (errorStr.Contains("(422)"))
                        {
                            lblSubmitStatus.Text = "The request was well-formed but could not be followed due to semantic errors. Verify the data provided for correctness and completeness.";
                        }
                        else if (errorStr.Contains("(429)"))
                        {
                            lblSubmitStatus.Text = "Too many requests were sent in a given timeframe. Implement request throttling and respect rate limits.";
                        }
                        else
                        {
                            lblSubmitStatus.Text = "Something went wrong, try again.";
                        }

                        lblSubmitStatus.Visible = true;
                        return;
                    }
                }

                //load current database
                XmlDocument doc = new XmlDocument();
                string path = Server.MapPath("~/App_Data/TicketsDatabase.xml");
                doc.Load(path);
                XmlNode root = doc.DocumentElement;
                string xpath = "/Tickets/Ticket/TicketNumber";
                var myNode = doc.SelectNodes(xpath);

                //Find next Ticket Number available
                int NextTicket = myNode.Count +1;

                //Make New Element
                XmlElement Ticket = doc.CreateElement("Ticket");
                XmlElement TicketNumber = doc.CreateElement("TicketNumber");
                TicketNumber.InnerText = NextTicket.ToString();
                XmlElement Requester = doc.CreateElement("RequestingUsername");
                Requester.InnerText = "TEST" + NextTicket.ToString();   //TODO fix this with coookies to figure otu who is requesting
                XmlElement Text = doc.CreateElement("Text");
                Text.InnerText = txtIssueBox.Text;
                XmlElement Image = doc.CreateElement("Image");
                Image.InnerText = img;
                XmlElement Status = doc.CreateElement("Status");
                Status.InnerText = "Open";

                Ticket.AppendChild(TicketNumber);
                Ticket.SetAttribute("Category","Updates");   //To do make this smart with chris's code
                Ticket.AppendChild(Requester);
                Ticket.AppendChild(Text);
                Ticket.AppendChild(Image);
                Ticket.AppendChild(Status);

                root.AppendChild(Ticket);

                doc.Save(path);
            }
            else
            {
                lblSubmitStatus.Visible = false;
                lblSubmitStatus.Text = "Missing Required information to submit a ticket";
            }
        }

        protected void lblLogout_Click(object sender, EventArgs e)
        {
            //Todo Add logic to logout here
            Server.Transfer("DefaultPage.aspx");
        }
    }
}