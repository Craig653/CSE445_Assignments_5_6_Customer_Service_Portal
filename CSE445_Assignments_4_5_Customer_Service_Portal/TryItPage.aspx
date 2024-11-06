<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="TryItPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm1" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1" style="background-color: #000099">
            <h1>Try it Page</h1>
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
        <p class="auto-style2">
            Captcha Image:
            <asp:Image ID="CaptchaImg" runat="server" style="height: 30px" Visible="False" />
        </p>
        <p style="width: 534px; height: 36px;">
            <span class="auto-style2">Image String Length is</span>
            <asp:TextBox ID="StringLenTxtBx" runat="server">4</asp:TextBox>
            <asp:Button ID="btnShowImage" runat="server" OnClick="btnShowImage_Click" Text="Get Captcha" />
        </p>
        <p style="width: 534px">
            <span class="auto-style2">Enter the string here</span>
            <asp:TextBox ID="CaptchaEnterTxtBx" runat="server"></asp:TextBox>
            &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" style="height: 26px" Text="Submit" />
        </p>
        <p style="width: 534px">
            <span class="auto-style2">
            <asp:Label ID="VerificationLbl" runat="server"></asp:Label>
        </p>
        <h3>
            <strong>Local Component 2 - Cookies</strong></span></h3>
        <p class="auto-style2">
            Craig to implement some sort of cookie logic here</p>
    </form>
    <hr />
    <h2><strong>Kiera&#39;s Services/Components</strong></h2>
    <hr />
    <h2><strong>Chris&#39;s Services/Components</strong></h2>
    <hr />
</body>
</html>
