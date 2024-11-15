<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="TryItPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm1" %>
<%@ Register TagPrefix ="cse" TagName= "Captcha" Src="~/CaptchaImage.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
        #form1 {
            font-weight: 700;
        }
        .auto-style2 {
            font-weight: normal;
        }
        .auto-style3 {
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1" style="background-color: #000099">
            <h1>Try it Page&nbsp;&nbsp;
                <asp:Button ID="btnDefaultPage" runat="server" OnClick="btnDefaultPage_Click" Text="Default Page" />
            </h1>
        </div>
        <h2><strong>Craig&#39;s Services/Components</strong></h2>
        <h3><strong>Service 1 Groq AI (2 Functions, Chat and Image Recognizer)</strong></h3>
        <strong>AskGroq (REST)</strong><br />
        <span class="auto-style2">This Service is connected to Groq. A Fast AI Inference using the mixtral-8x7b-32768 model. More details can be found at <a href="https://groq.com/">https://groq.com/</a>
        <br />
        URL: <a href="http://localhost:63092/Service1.svc">http://localhost:63092/Service1.svc</a> or http://localhost:63092/Service1.svc/AskGroq/{string}</span><br />
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
        Image Recognizer Groq: (WSDL)<br />
        </strong><span class="auto-style2">This service is connected to Groq Visualizer. Using the llama-3.2-11b-vision-preview model. More details can be found at <a href="https://console.groq.com/docs/vision">https://console.groq.com/docs/vision</a>
        <br />
        URL: <a href="http://localhost:56274/Service1.svc">http://localhost:56274/Service1.svc</a></span><br />
        <span class="auto-style2">Method: ImgGroq(String image in base64)<br />
        Returns string<br />
        <br />
        Image Recognizer (.JPEG only) (&lt;25kb images)<br />
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
        <h3>Local Component 1 - Captcha Generator</h3>
        <p class="auto-style2">
            This Service connects to ASU&#39;s string image generator. This will be used to validate people are not robots upon creating a password</p>
        <cse:Captcha runat ="server"/>
        <h3>
            <strong>Local Component 2 - Cookies</strong></h3>
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
                    <asp:TreeNodeBinding DataMember="TicketNumber" Depth="1" FormatString="TicketNumber: {0}" SelectAction="Expand" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="RequestingUsername" Depth="1" FormatString="Requester: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Text" FormatString="Description: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Image" Depth="1" SelectAction="None" TextField="#Name" ToolTipField="#Value" />
                    <asp:TreeNodeBinding DataMember="Status" Depth="1" FormatString="Status: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
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
        <h3><strong>Service 1 - Get Most Common Ticket Category (RESTful Service)</strong></h3>
        <strong>Get Most Common Ticket Category (REST)</strong><br />
        <span class="auto-style2">
            This service retrieves the most common category of ticket issues from the ticket database.<br />
            URL: <a href="http://localhost:44343/api/tickets/mostcommoncategory">http://localhost:44343/api/tickets/mostcommoncategory</a><br />
            Method: HTTP GET<br />
            Returns: String<br />
            <br />
            Get Most Common Ticket Category Service<br />
        </span>
        <span class="auto-style2">Ticket Category:</span>
        <asp:Button ID="btnGetMostCommonCategory" runat="server" Text="Get Most Common Ticket Category (REST)" OnClick="btnGetMostCommonCategory_Click" /><br /><br />
        <span class="auto-style2"><strong>Ticket Service Response:</strong></span><br />
        <asp:Label ID="lblResultCategory" runat="server" Text=""></asp:Label><br /><br />
         <h3>Local Component 1 - Global.asax Event Handlers</h3>
        <strong>Application Start Time:</strong> <asp:Label ID="lblAppStartTime" runat="server"></asp:Label><br />
        <strong>Application End Time (Last Run):</strong> <asp:Label ID="lblAppEndTime" runat="server"></asp:Label><br />
        <strong>Session Start Time:</strong> <asp:Label ID="lblSessionStartTime" runat="server"></asp:Label><br /><br />
        <hr />
        <h2><strong>Chris's Services/Components</strong></h2>

    </form>
</body>
</html>
