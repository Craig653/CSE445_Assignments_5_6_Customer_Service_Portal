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

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Local Componment - Global.asax event handlers 
        protected void Page_Load(object sender, EventArgs e)
        {
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

        //Service 1.1: Ask Groq, ask groq any question. Its an AI chat bot. This will ball the REST API ASK groq. Then return a string reply
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

        //Service 3 IMG Groq, takes an image upload and uses groq to describe it in 25 words or less, along with some error catching
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

        protected void btnCookieCreator_Click(object sender, EventArgs e)
        {
            string value = txtbxCookieCreator.Text;


            HttpCookie mycookies = Request.Cookies[value];

            if((mycookies == null) || mycookies[value] == "")
            {
                HttpCookie newCookies = new HttpCookie(value);
                lblCookieCreatorStatus.Text = "Cookie Created!";
                newCookies[value] = value;
                newCookies.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(newCookies);
            }
            else
            {
                lblCookieCreatorStatus.Text = "Can't create Cookie, " + value + " is already created!";
            }
        }

        protected void btnLookup_Click(object sender, EventArgs e)
        {
            string value = txtboxCookieLookup.Text;
            HttpCookie mycookies = Request.Cookies[value];

            if ((mycookies == null) || mycookies[value] == "")
            {
                lblCookieRetStatus.Text = "Cookie Doesn't Exist!";
            }
            else
            {
                lblCookieRetStatus.Text = "Found Cookie: " + value + "";
            }
        }
        protected void btnGetMostCommonCategory_Click(object sender, EventArgs e)
        {
            try
            {
                // URL to the RESTful API endpoint
                string serviceUrl = "http://localhost:44343/api/tickets/mostcommoncategory";

                using (WebClient client = new WebClient())
                {
                    // Make sure the request expects JSON data
                    client.Headers.Add("Content-Type", "application/json");
                    string response = client.DownloadString(serviceUrl);
                    lblResultCategory.Text = $"Most Common Ticket Category (REST): {response}";
                }
            }
            catch (Exception ex)
            {
                lblResultCategory.Text = $"Error: {ex.Message}";
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