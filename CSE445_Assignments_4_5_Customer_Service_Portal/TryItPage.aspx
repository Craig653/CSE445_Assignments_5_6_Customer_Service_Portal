<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryItPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1" style="background-color: #000099">
            <h1>Try it Page</h1>
        </div>
        <h2><strong>Craig&#39;s Services/Components</strong></h2>
        <p>
            <strong>Service 1 Groq AI (2 Functions)</strong></p>
        <strong>AskGroq (REST)</strong><br />
        This Service is connected to Groq. A Fast AI Inference using the mixtral-8x7b-32768 model. More details can be found at <a href="https://groq.com/">https://groq.com/</a>
        <br />
        URL: <a href="http://localhost:63092/Service1.svc">http://localhost:63092/Service1.svc</a> or http://localhost:63092/Service1.svc/AskGroq/{string}<br />
        Method: AskGroq(String)<br />
        Returns String<br />
        <br />
        Ask Groq Service<br />
        Chat:
        <asp:TextBox ID="txtbxGroq" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnGroq" runat="server" OnClick="btnGroq_Click" Text="AskGroq" />
        <br />
        Groq Response:
        <br />
        <asp:Label ID="lblAskGroq" runat="server" Visible="False"></asp:Label>
        <strong>
        <br />
        <br />
        <br />
        Image Recognizer Groq: (WSDL)<br />
        </strong>This service is connected to Groq Visualizer. Using the llama-3.2-11b-vision-preview model. More details can be found at <a href="https://console.groq.com/docs/vision">https://console.groq.com/docs/vision</a>
        <br />
        URL: <a href="http://localhost:56274/Service1.svc">http://localhost:56274/Service1.svc</a><br />
        Method: ImgGroq(String image in base64)<br />
        Returns string<br />
        <br />
        Image Recognizer (.JPEG only) (&lt;25kb images)<br />
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <br />
        <asp:Button ID="btnIMGGroq" runat="server" OnClick="btnIMGGroq_Click" Text="ImageGroq" />
        <br />
        Groq Response:
        <br />
        <asp:Label ID="lblImageGroq" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
