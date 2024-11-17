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
using LocalHash;
using System.Xml.Linq;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // usernames harcoded in this function, then hashed and stored in XML file
            PreCreateHashes();
            CheckForAutomaticLogin();
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


            // Perform automatic login if possible
            AutoLogin();     

            // Handle TreeView Filtering Based on Cookie
            LoadTreeViewFilter();

            CheckForAutomaticLogin();


            // Kiera's Local Componment 1 - Global.asax event handlers 
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
                //Uri baseUri = new Uri("http://localhost:63092/Service1.svc");
                Uri baseUri = new Uri("http://webstrar10.fulton.asu.edu/page1/Service1.svc");
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


                if (myParsedConent != null)
                {
                    lblAskGroq.Text = myParsedConent.choices[0].message.content;
                    lblAskGroq.Visible = true;
                }
                else
                {
                    lblAskGroq.Text = "Oh No! Looks like Groq AI might be down, try again later. Or check Groq.AI for there service status";
                    lblAskGroq.Visible = true;
                }

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

                    if (myParsedConent != null)
                    {
                        lblImageGroq.Text = myParsedConent.choices[0].message.content;
                        lblImageGroq.Visible = true;
                    }
                    else
                    {
                        lblImageGroq.Text = "Oh No! Looks like Groq AI might be down, try again later. Or check Groq.AI for there service status";
                        lblImageGroq.Visible = true;
                    }

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

        protected void LoadTreeViewFilter()
        {
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

            // For TreeView reloading
            TreeView1.DataSourceID = "";
            TreeView1.DataSourceID = "XmlDataSource1";
            XmlDataSource1.XPath = xpath;
        }

        private void AutoLogin()
        {
            HttpCookie loginCookie = Request.Cookies["AutoLogin"];
            if (loginCookie != null && !string.IsNullOrEmpty(loginCookie["Username"]))
            {
                // If user has already logged in, automatically log them in
                lblAutoLoginStatus.Text = $"Welcome back, {loginCookie["Username"]}!";
                pnlLoginForm.Visible = false;
                pnlLogout.Visible = true;
            }
            else
            {
                // If no login cookie, show the create account form
                pnlLoginForm.Visible = true;
                pnlLogout.Visible = false;
            }
        }

        private void CheckForAutomaticLogin()
        {
            // Check if user cookies are present for automatic login
            if (Request.Cookies["UserCredentials"] != null)
                {
                    string username = Request.Cookies["UserCredentials"]["Username"];
                    string password = Request.Cookies["UserCredentials"]["Password"];

                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        // Automatically populate the username and password fields
                        txtUsername.Text = username;
                        txtPassword.Attributes["value"] = password; // Populate the password field

                        // Automatically log the user in
                        pnlLoginForm.Visible = false;
                        pnlLogout.Visible = true;
                        lblAutoLoginStatus.Text = $"Welcome back, {username}!";
                    }
                }
                else
                {
                    pnlLoginForm.Visible = true;
                    pnlLogout.Visible = false;
                }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Store username and password in a cookie
                HttpCookie userCredentialsCookie = new HttpCookie("UserCredentials");
                userCredentialsCookie["Username"] = username;
                userCredentialsCookie["Password"] = password;
                userCredentialsCookie.Expires = DateTime.Now.AddMonths(6);
                Response.Cookies.Add(userCredentialsCookie);

                // Log the user in and update UI
                pnlLoginForm.Visible = false;
                pnlLogout.Visible = true;
                lblAutoLoginStatus.Text = $"Welcome back, {username}!";
            }
            else
            {
                lblAutoLoginStatus.Text = "Please enter a valid username and password.";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear the user credentials cookie
            if (Request.Cookies["UserCredentials"] != null)
            {
                HttpCookie userCredentialsCookie = new HttpCookie("UserCredentials");
                userCredentialsCookie.Expires = DateTime.Now.AddMonths(-1); // Expire the cookie
                Response.Cookies.Add(userCredentialsCookie);
            }

            // Reset the UI
            pnlLoginForm.Visible = true;
            pnlLogout.Visible = false;
            lblAutoLoginStatus.Text = "";
        }

        // Chris's Service: Define most common category for every Ticket "Text" (issue summary) element
        protected void testAttrbteUpdateBtn_Click(object sender, EventArgs e)
        {
            ServiceReference2.Service1Client categorizeSummariesProxy = new ServiceReference2.Service1Client();
            categorizeSummariesProxy.MostCommon();
            testAttrbteUpdateTxtBox.Text = Server.HtmlEncode(categorizeSummariesProxy.GetCurrentXML()); // Display the XML pweety
            categorizeSummariesProxy.Close();
        }

        // precreate credential hashes for the XML database
        private void PreCreateHashes()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/");
            string localFile = Path.Combine(localDir, "CredentialsDatabase.xml");

            // Ensure the directory exists
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // Ensure the file exists
            if (!File.Exists(localFile))
            {
                File.Create(localFile).Dispose(); // Create the file and dispose to release the handle
            }

            XDocument xmlDoc = new XDocument(
                new XElement("CredentialsDatabase",
                    new XElement("Credentials",
                        new XElement("Username", "TA"),
                        new XElement("Password", EncryptPassword("Cse445!")),
                        new XAttribute("UserType", "Admin")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Chris"),
                        new XElement("Password", EncryptPassword("Password123")),
                        new XAttribute("UserType", "Customer")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Craig"),
                        new XElement("Password", EncryptPassword("123Password")),
                        new XAttribute("UserType", "Customer")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Kiera"),
                        new XElement("Password", EncryptPassword("Pass123word")),
                        new XAttribute("UserType", "Customer")
                    )
                )
            );
            xmlDoc.Save(localFile);
        }

        // helper for encrypting passwords
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt();
            return encryptor.EncryptString(password);
        }

        //Chris's Service: Encrypt customer credentials in XML database 
        protected void custoLoginBtn_Click(object sender, EventArgs e)
        {
            var attemptUsername = custoUsrNmeTxtBox.Text;
            var attemptPassword = custoPasswdTxtBox.Text;

            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/");
            string localFile = Path.Combine(localDir, "CredentialsDatabase.xml");

            // Ensure the directory exists
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // Ensure the file exists
            if (!File.Exists(localFile))
            {
                File.Create(localFile).Dispose(); // Create the file and dispose to release the handle
            }

            XDocument xmlDoc = XDocument.Load(localFile);

            // Encrypt the attempted password
            CredentialEncrypt encryptor = new CredentialEncrypt();
            string encryptedAttemptPassword = encryptor.EncryptString(attemptPassword);

            // Iterate through each <Credentials> element
            foreach (var user in xmlDoc.Descendants("Credentials"))
            {
                var usernameElement = user.Element("Username");
                var passwordElement = user.Element("Password");
                var userAttribute = user.Attribute("UserType");

                if (userAttribute.Value == "Customer") // credentials belong to a customer
                {
                    // Check if the username and password match
                    if (usernameElement.Value == attemptUsername && passwordElement.Value == encryptedAttemptPassword)
                    {
                        // Log the user in
                        lblCustoLoginStatus.Text = "Login successful!";
                        return;
                    } else
                    {
                        // If no match found
                        lblCustoLoginStatus.Text = "Login failed. Invalid username or password.";
                    }
                }
            }
        }


        //Chris's Service: Encrypt admin credentials in XML database
        protected void adminLoginBtn_Click(object sender, EventArgs e)
        {
            var attemptUsername = adminUsrNmeTxtBox.Text;
            var attemptPassword = adminPasswdTxtBox.Text;

            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/");
            string localFile = Path.Combine(localDir, "CredentialsDatabase.xml");

            // Ensure the directory exists
            if (!Directory.Exists(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // Ensure the file exists
            if (!File.Exists(localFile))
            {
                File.Create(localFile).Dispose(); // Create the file and dispose to release the handle
            }

            XDocument xmlDoc = XDocument.Load(localFile);

            // Encrypt the attempted password
            CredentialEncrypt encryptor = new CredentialEncrypt();
            string encryptedAttemptPassword = encryptor.EncryptString(attemptPassword);

            // Iterate through each <Credentials> element
            foreach (var user in xmlDoc.Descendants("Credentials"))
            {
                var usernameElement = user.Element("Username");
                var passwordElement = user.Element("Password");
                var userAttribute = user.Attribute("UserType");

                if (userAttribute.Value == "Admin") // credentials belong to a customer
                {
                    // Check if the username and password match
                    if (usernameElement.Value == attemptUsername && passwordElement.Value == encryptedAttemptPassword)
                    {
                        // Log the user in
                        lblAdminLoginStatus.Text = "Login successful!";
                        return;
                    } else
                    {
                        // If no match found
                        lblAdminLoginStatus.Text = "Login failed. Invalid username or password.";
                    }
                }
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