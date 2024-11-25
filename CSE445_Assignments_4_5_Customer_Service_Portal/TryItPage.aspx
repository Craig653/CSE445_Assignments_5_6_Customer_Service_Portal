<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="TryItPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm1" Async="true" %>
<%@ Register TagPrefix ="cse" TagName= "Captcha" Src="~/CaptchaImage.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, intial-scale=1, shrink-to-fit= no" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
</head>
<body>
    <nav class="navbar navbar-expand-lg bg-primary" data-bs-theme="dark">
        <div class="container-fluid">
        <a class="navbar-brand">Customer Service App</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarColor01">
            <ul class="navbar-nav me-auto">
            <li class="nav-item">
                <a class="nav-link" id="btnDefault" runat="server" onserverclick="btnDefault_Click">Home</a>
            </li>
            <li class="nav-item">
                <a class="nav-link " id="btnMemberLogin" runat="server" onserverclick="btnLoginMember_Click">Member</a>
            </li>
            <li class="nav-item">
                <a class="nav-link " id="btnLoginAgent" runat="server" onserverclick="btnLoginAgent_Click">Agent</a>
                <span class="visually-hidden">(current)</span>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnLoginStaff" runat="server" onserverclick="btnLoginStaff_Click">Staff</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" id="btnTryIt" runat="server" onserverclick="btnTryIt_Click">TryIt Page</a>
            </li>
            <li class="nav-item">
                  <a class="nav-link" id="btnComponentTable" runat="server" onserverclick="btnComponentTable_Click">Component Table</a>
            </li>
            </ul>
        </div>
        <div class="pull-right">
            <ul class="nav navbar-nav">
                <li>
                    <a class="nav-link" id="Login" runat="server" onserverclick="btnLoginOut_Click">Login</a>
                </li>
            </ul>     
        </div> 
        </div>
    </nav>

    <form id="form1" runat="server" style="margin: 20px;">

        <h2><strong>Craig&#39;s Services/Components</strong></h2>
        <strong>Service 1 Groq AI (2 Functions, Chat and Image Recognizer)<br />
        AskGroq (REST)</strong><br />
        <span class="auto-style2">This Service is connected to Groq. A Fast AI Inference using the mixtral-8x7b-32768 model. More details can be found at <a href="https://groq.com/">https://groq.com/</a>
        <br />
        This it the <strong>generic chat connection</strong>, it will be used to analyze the description. See the <strong>Agent page</strong> for implementation and try it to tickets database.<br />
        URL: <a href="http://webstrar10.fulton.asu.edu/page0/Service1.svc">http://webstrar10.fulton.asu.edu/page1/Service1.svc</a>&nbsp; or http://localhost:63092/Service1.svc/AskGroq/{string}</span><br />
        <span class="auto-style2">Method: AskGroq(String)<br />
        Returns String<br />
        </span>
        <br />
        Ask Groq Service<br />
        <span class="auto-style2">Chat:</span>
        <asp:TextBox ID="txtbxGroq" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnGroq" runat="server" OnClick="btnGroq_Click" Text="AskGroq" />
        <br />
        <span class="auto-style2"><strong>Groq Response:
        </strong>
        <br />
        <asp:Label ID="lblAskGroq" runat="server" Visible="False"></asp:Label>
        </span>
        <strong>
        <br />
        <br />
        <br />
                    <hr />
        Image Recognizer Groq: (WSDL)<br />
        </strong><span class="auto-style2">This service is connected to Groq Visualizer. Using the llama-3.2-11b-vision-preview model. More details can be found at <a href="https://console.groq.com/docs/vision">https://console.groq.com/docs/vision</a>
        <br />
        This it the <strong>generic chat connection</strong>, it will be used to analyze the description. See the <strong>Agent page</strong> for implementation and try it to tickets database.<br />
        URL: </span><a href="http://webstrar10.fulton.asu.edu/page0/Service1.svc">http://webstrar10.fulton.asu.edu/page0/Service1.svc</a> <br />
        <span class="auto-style2">Method: ImgGroq(String image in base64)<br />
        Returns string<br />
        <br />
        Image Recognizer (.JPEG only) (&lt;25kb images) (see example folder included in project for sample images)<br />
        </span>
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <br />
        <asp:Button ID="btnIMGGroq" runat="server" OnClick="btnIMGGroq_Click" Text="ImageGroq" />
        <br />
        <span class="auto-style2"><strong>Groq Response:
        </strong>
        <br />
        <asp:Label ID="lblImageGroq" runat="server" Visible="False"></asp:Label>
        </span>
        <br />
                <hr />
        <h3>Local Component 1 - Captcha Generator</h3>
        <p class="auto-style2">
            This Service connects to ASU&#39;s string image generator. This will be used to validate people are not robots upon creating a password.</p>
        <cse:Captcha runat ="server"/>
        <h3>
            <strong>Local Component 2 - Cookies</strong></h3>
        <p>
            <span class="auto-style3">This component will create a username cookie and will filter the tickets Tree view based on the username. </p>
        <p>
            See the member page for the full implementation. (Be sure to save a cookie with a username first)</span></p>
        <p>
            Cookie Creator</p>
        <p class="auto-style3">
            Username:
            <asp:TextBox ID="txtbxCookieCreator" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnCookieCreator" runat="server" OnClick="btnCookieCreator_Click" Text="Create" />
        </p>
        <p class="auto-style3">
            Status: <asp:Label ID="lblCookieCreatorStatus" runat="server"></asp:Label>
        </p>
        <p>
            Cookie Retriever</p>
        <p class="auto-style3">
            Username Cookie Lookup
&nbsp;<asp:Button ID="btnLookup" runat="server" OnClick="btnLookup_Click" Text="Lookup" />
        </p>
        <p class="auto-style3">
            Username Cookie: <asp:Label ID="lblCookieRetStatus" runat="server"></asp:Label>
        </p>
        <p>
            Tree Viewer (<asp:Label ID="lblFilterBy" runat="server"></asp:Label>
            )&nbsp;
            <asp:Button ID="lblResetCookie" runat="server" OnClick="lblResetCookie_Click" Text="Delete Cookie" />
        </p>
        <p>
            <strong>
            <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" ImageSet="Simple" NodeIndent="10" Width="367px">
                <DataBindings>
                    <asp:TreeNodeBinding DataMember="TicketNumber" Depth="1" FormatString="TicketNumber: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="RequestingUsername" Depth="1" FormatString="Requester: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Text" FormatString="Description: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Image" Depth="1" SelectAction="None" TextField="#Name" ToolTipField="#Value" />
                    <asp:TreeNodeBinding DataMember="Status" Depth="1" FormatString="Status: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Ticket" SelectAction="None" TextField="#Name" />
                </DataBindings>
                <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/TicketsDatabase.xml" XPath="/Tickets/Ticket"></asp:XmlDataSource>
            </strong></p>
        <p>
            &nbsp;</p>
        <hr />
        <h2><strong>Kiera's Services/Components</strong></h2>
        <h3><strong>Service 1 - Get Most Common Ticket Category</strong></h3>
        <strong>Get Most Common Ticket Category</strong><br />
        <strong>Ticket Processing Service - Get Most Common Category</strong><br />
        <span class="auto-style2">
            This service reads the ticket XML database and finds the most common category of tickets.<br />
            URL: <a href="http://localhost:44360/Service1.svc">http://localhost:44360/Service1.svc</a><br />
            Method: WCF Method Call<br />
            Returns: String<br />
            <br />
            Get Most Common Ticket Category
        </span>
        <p>
            <asp:Button ID="btnGetMostCommonCategory" runat="server" Text="Get Most Common Ticket Category" OnClick="btnGetMostCommonCategory_Click" />
        </p>
        <p>
            <asp:Label ID="lblMostCommonCategoryResult" runat="server" Text=""></asp:Label>
        </p>
        <h3>Local Component 1 - Global.asax Event Handlers</h3>
        <p>
            This component displays the application event times, such as the start time of the application, the end time of the last run, and session start times. It helps track application lifecycle events for diagnostics and logging.
        </p>
        <strong>Application Start Time:</strong> <asp:Label ID="lblAppStartTime" runat="server"></asp:Label><br />
        <strong>Application End Time (Last Run):</strong> <asp:Label ID="lblAppEndTime" runat="server"></asp:Label><br />
        <strong>Session Start Time:</strong> <asp:Label ID="lblSessionStartTime" runat="server"></asp:Label><br /><br />
        <hr />
        <h3>Local Component 2 - Automatic Login System (Cookies)</h3>
        <p>
            This component allows users to create an account and automatically log in if their credentials are saved in a cookie. If no credentials are stored, the user is prompted to enter their username and password. This feature streamlines the login process, ensuring easy and seamless access.
        </p>
        <div>
            <asp:Panel ID="pnlLoginForm" runat="server" Visible="true">
            <strong>Create Account / Log In:</strong><br />
            Username: <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />
            Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account / Log In" OnClick="btnCreateAccount_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlLogout" runat="server" Visible="false">
            <asp:Label ID="lblAutoLoginStatus" runat="server" Text=""></asp:Label><br />
            <asp:Button ID="btnLogout" runat="server" Text="Log Out" OnClick="btnLogout_Click" />
        </asp:Panel>
        </div>
        <hr />
        <h2><strong>Chris's Services/Components</strong></h2>
        <p>
            Service 1 - Designate Most Common Ticket Category. This WSDL service reads the full XML Tickets Database, reads the &quot;Text&quot; element of this XML file, which is the summary of issue described by the customer, and updates the &quot;Category&quot; attribute with the most common, non-stopword in the the summary. This should ideally be trigerred everytime a user makes a change to the &quot;Text&quot; element in their ticket.</p>
        <p>
            To be able to see how this service is working, please open the local&quot;Serv1Serv14Compare/App_Data/TicketsDatabase.xml&quot; file in this service in any text editor, and feel free to make changes in the &quot;Text&quot; element of each Ticket parent element. The most common non-stop word for each Ticekt summary will be considered the main category of such ticket, and such will be updated in the &quot;Category&quot; attribute for each ticket. The Text Box below displays the most recent TicketsDatabase.xml.</p>
        <p>
            <asp:Label ID="testAttrbteUpdateLbl" runat="server" Text="Test attribute update:"></asp:Label>
            <asp:Button ID="testAttrbteUpdateBtn" runat="server" OnClick="testAttrbteUpdateBtn_Click" Text="Triger attribute update" />
        </p>
        <p>
            <asp:TextBox ID="testAttrbteUpdateTxtBox" runat="server" Height="527px" Width="2027px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
        </p>
        <hr />
        <p>
            <asp:Label ID="custoLoginTitleLbl" runat="server" Text="Customer Login"></asp:Label>
        </p>
        <p>
            Component 1: This component uses a DLL implemented from a custom AES encryption implementation using the CSharp Cryptography library. Inilialization vector and secret key were arbitrarily imposed. This componentcreates the assumption of existing user credentials by hard-coding usernames and passwords expected at page load for both customers and admins, using the DLL to encrypt the password, and store the encrypted password along with its pertaining username in an xml database. When the customer tries to login, the implementation will verify that a customer is indeed the user type attempting to login, then hash the provided password by the user and compare this password hash to the ones saved in the XML database, where the username must also be the correct username for such hash.</p>
        <p>
            Some Customer Credentials you can test that were preloaded:</p>
        <p>
            (&quot;Username&quot;, &quot;Chris&quot;), (&quot;Password&quot;, Password123&quot;) |&nbsp; (&quot;Username&quot;, &quot;Craig&quot;), (&quot;Password&quot;, 123Password&quot;) |&nbsp; (&quot;Username&quot;, &quot;Kiera&quot;), (&quot;Password&quot;, Pass123word&quot;)</p>
        <p>
            XML Database is located in &quot;CSE445_Assignments_5_6_Customer_Service_Portal/App_Data/CredentialsDatabase.xml&quot;</p>
        <asp:Label ID="custoUsrNmeLbl" runat="server" Text="Customer Username:"></asp:Label>
        <asp:TextBox ID="custoUsrNmeTxtBox" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="custoPasswdLbl" runat="server" Text="Customer Password:"></asp:Label>
            <asp:TextBox ID="custoPasswdTxtBox" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="custoLoginBtn" runat="server" Text="Customer Login" OnClick="custoLoginBtn_Click" />
        <asp:Label ID="lblCustoLoginStatus" runat="server" Text="Customer Login Status"></asp:Label>
        <hr />
        <br />
        <asp:Label ID="adminLoginTitleLbl" runat="server" Text="Admin Login"></asp:Label>
        <br />
        Component 2: This component uses a DLL implemented from a custom AES encryption implementation using the CSharp Cryptography library. Inilialization vector and secret key were arbitrarily imposed. This componentcreates the assumption of existing user credentials by hard-coding usernames and passwords expected at page load for both customers and admins, using the DLL to encrypt the password, and store the encrypted password along with its pertaining username in an xml database. When the admin tries to login, the implementation will verify that a customer is indeed the user type attempting to login, then hash the provided password by the user and compare this password hash to the ones saved in the XML database, where the username must also be the correct username for such hash.<br />
        <br />
        Some Admin Credentials you can test that were preloaded:<br />
        <br />
        (&quot;Username&quot;, &quot;TA&quot;), (&quot;Password&quot;, &quot;Cse445!&quot;)<br />
        <br />
        XML Database is located in &quot;CSE445_Assignments_5_6_Customer_Service_Portal/App_Data/CredentialsDatabase.xml&quot; <p>
            <asp:Label ID="adminUsrNmeLbl" runat="server" Text="Admin Username:"></asp:Label>
            <asp:TextBox ID="adminUsrNmeTxtBox" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="adminPasswdLbl" runat="server" Text="Admin Password:"></asp:Label>
            <asp:TextBox ID="adminPasswdTxtBox" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="adminLoginBtn" runat="server" Text="Admin Login" OnClick="adminLoginBtn_Click" />
        <asp:Label ID="lblAdminLoginStatus" runat="server" Text="Admin Login Status"></asp:Label>
    </form>
</body>
</html>