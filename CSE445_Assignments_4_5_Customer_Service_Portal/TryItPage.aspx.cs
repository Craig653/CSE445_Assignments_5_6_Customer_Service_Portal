using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Web;
using System.Drawing.Printing;
using System.Net.Http;
using System.ServiceModel.Security;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Craig's Tree view filtering used in conjuction with the username cookie
            string username = "";
            string xpath = "//Tickets/Ticket[RequestingUsername[text()=\"\"]]";
            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
                username = userCookie.Value.ToString();
                username = username.Split('=')[1];
                xpath = "//Tickets/Ticket[RequestingUsername[text()=\"" + username + "\"]]";
                lblFilterBy.Text = "Filtered by " + username;
            }
            else
            {
                xpath = "//Tickets/Ticket";
                lblFilterBy.Text = "No Filter";
            }

            //For tree view reloading
            TreeView1.DataSourceID = "";
            TreeView1.DataSourceID = "XmlDataSource1";

            XmlDataSource1.XPath = xpath;


            // Local Componment - Global.asax event handlers 
            if (!IsPostBack)
            {
                // Display Application Start Time
                if (Application["AppStartTime"] != null)
                {
                    lblAppStartTime.Text = $"Application Start Time: {Application["AppStartTime"]}";
                }
                else
                {
                    lblAppStartTime.Text = "Not Available";
                }

                // Display Application End Time (from the previous shutdown)
                if (Application["AppEndTime"] != null)
                {
                    lblAppEndTime.Text = $"Application End Time (Last Run): {Application["AppEndTime"]}";
                }
                else
                {
                    lblAppEndTime.Text = " Not Available";
                }

                // Display Session Start Time
                if (Session["SessionStartTime"] != null)
                {
                    lblSessionStartTime.Text = $"Session Start Time: {Session["SessionStartTime"]}";
                }
                else
                {
                    lblSessionStartTime.Text = " Available";
                }
            }
        }


        //Craig's Service 1.1: Ask Groq, ask groq any question. Its an AI chat bot. This will ball the REST API ASK groq. Then return a string reply
        //Start AskGroq service to use this
        protected void btnGroq_Click(object sender, EventArgs e)
        {
            if (txtbxGroq.Text.Length > 0)
            {
                string text = txtbxGroq.Text;
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
                        lblAskGroq.Text = "The server could not understand the request due to invalid syntax. Review the request format and ensure it is correct.";
                    }
                    else if (errorStr.Contains("(401)"))
                    {
                        lblAskGroq.Text = "The request was not successful because it lacks valid authentication credentials for the requested resource. Ensure the request includes the necessary authentication credentials and the api key is valid.";
                    }
                    else if (errorStr.Contains("(404)"))
                    {
                        lblAskGroq.Text = "The requested resource could not be found. Check the request URL and the existence of the resource.";
                    }
                    else if (errorStr.Contains("(422)"))
                    {
                        lblAskGroq.Text = "The request was well-formed but could not be followed due to semantic errors. Verify the data provided for correctness and completeness.";
                    }
                    else if (errorStr.Contains("(429)"))
                    {
                        lblAskGroq.Text = "Too many requests were sent in a given timeframe. Implement request throttling and respect rate limits.";
                    }

                    lblAskGroq.Visible = true;
                    return;
                }

                Stream strm = new MemoryStream(abc);
                DataContractSerializer obj = new DataContractSerializer(typeof(string));
                string filteredText = obj.ReadObject(strm).ToString();

                Root myParsedConent = JsonSerializer.Deserialize<Root>(filteredText);


                lblAskGroq.Text = myParsedConent.choices[0].message.content;
                lblAskGroq.Visible = true;

            }

        }

        //Craig's Service 1.2 IMG Groq, takes an image upload and uses groq to describe it in 25 words or less, along with some error catching
        //Start ImgGroqService to use this
        protected void btnIMGGroq_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFile)
            {
                string filename = FileUpload2.FileName;

                string extention = Path.GetExtension(filename);
                if (extention != ".jpeg")
                {
                    lblImageGroq.Text = "Not a JPEG File Try again";
                    lblImageGroq.Visible = true;
                }
                else
                {
                    string img = Convert.ToBase64String(FileUpload2.FileBytes);

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
                            lblImageGroq.Text = "The server could not understand the request due to invalid syntax. Review the request format and ensure it is correct.";
                        }
                        else if (errorStr.Contains("(401)"))
                        {
                            lblImageGroq.Text = "The request was not successful because it lacks valid authentication credentials for the requested resource. Ensure the request includes the necessary authentication credentials and the api key is valid.";
                        }
                        else if (errorStr.Contains("(404)"))
                        {
                            lblImageGroq.Text = "The requested resource could not be found. Check the request URL and the existence of the resource.";
                        }
                        else if (errorStr.Contains("(413)"))
                        {
                            lblImageGroq.Text = "The remote server returned an unexpected response: (413) Request Entity Too Large. Files smaller than <25kb only!";
                        }
                        else if (errorStr.Contains("(422)"))
                        {
                            lblImageGroq.Text = "The request was well-formed but could not be followed due to semantic errors. Verify the data provided for correctness and completeness.";
                        }
                        else if (errorStr.Contains("(429)"))
                        {
                            lblImageGroq.Text = "Too many requests were sent in a given timeframe. Implement request throttling and respect rate limits.";
                        }
                        else
                        {
                            lblImageGroq.Text = "Something went wrong, try again.";
                        }

                        lblImageGroq.Visible = true;
                        return;
                    }

                    Root myParsedConent = JsonSerializer.Deserialize<Root>(jsonStr);

                    lblImageGroq.Text = myParsedConent.choices[0].message.content;
                    lblImageGroq.Visible = true;
                }
            }

        }


        //Craig's Component 
        //Get the cookie named username
        //Create new cookie if needed else get the value
        protected void btnCookieCreator_Click(object sender, EventArgs e)
        {
            string value = txtbxCookieCreator.Text;


            HttpCookie mycookies = Request.Cookies["Username"];

            if((mycookies == null) || mycookies["Username"] == "")
            {
                HttpCookie newCookies = new HttpCookie("Username");
                lblCookieCreatorStatus.Text = "Cookie Created!";
                newCookies["Username"] = value;
                newCookies.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(newCookies);
                lblCookieCreatorStatus.Text = "Updated Username Cookie to: " + value;
                lblCookieRetStatus.Text = "";
                lblFilterBy.Text = "Filtered by " + value;
            }
            else
            {
                mycookies["Username"] = value;
                lblCookieCreatorStatus.Text = "Updated Username Cookie to: " + value;
                Response.Cookies.Add(mycookies);
                lblCookieRetStatus.Text = "";
                lblFilterBy.Text = "Filtered by " + value;
            }

            this.Page_Load(null, null);
        }

        //Look up cookie username
        protected void btnLookup_Click(object sender, EventArgs e)
        {
            HttpCookie mycookies = Request.Cookies["Username"];

            if ((mycookies == null) || mycookies["Username"] == "")
            {
                lblCookieRetStatus.Text = "Cookie Doesn't Exist!";
            }
            else
            {
                lblCookieRetStatus.Text = Request.Cookies["Username"].Value;
            }
            this.Page_Load(null, null);
        }

        //Delete the username cookie
        protected void lblResetCookie_Click(object sender, EventArgs e)
        {
            HttpCookie mycookies = Request.Cookies["Username"];

            if ((mycookies != null))
            {
                HttpCookie delCookie = new HttpCookie("Username");
                delCookie.Expires = DateTime.Now.AddMonths(-10);
                delCookie.Value = null;
                Response.Cookies.Add(delCookie);
                HttpContext.Current.Request.Cookies.Clear();
                lblFilterBy.Text = "No Filter";
            }
            //reload page to referesh tree
            this.Page_Load(null, null);
        }

        protected void btnDefaultPage_Click(object sender, EventArgs e)
        {
            Server.Transfer("DefaultPage.aspx");
        }
        protected void btnGetMostCommonCategory_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the WCF service client
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

                // Call the WCF service method to get the most common ticket category
                string mostCommonCategory = client.GetMostCommonCategory();

                // Display the result
                lblMostCommonCategoryResult.Text = $"Most Common Ticket Category: {mostCommonCategory}";

                // Close the client connection
                client.Close();
            }
            catch (Exception ex)
            {
                // Display an error message if something goes wrong
                lblMostCommonCategoryResult.Text = $"Error: {ex.Message}";
            }
        }
    }

 


    //Classes for JSON file object deseralization, starts at Root
    public class Choice
    {
        public int index { get; set; }
        public Message message { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
        public string system_fingerprint { get; set; }
        public XGroq x_groq { get; set; }
    }

    public class Usage
    {
        public double queue_time { get; set; }
        public int prompt_tokens { get; set; }
        public double prompt_time { get; set; }
        public int completion_tokens { get; set; }
        public double completion_time { get; set; }
        public int total_tokens { get; set; }
        public double total_time { get; set; }
    }

    public class XGroq
    {
        public string id { get; set; }
    }
}