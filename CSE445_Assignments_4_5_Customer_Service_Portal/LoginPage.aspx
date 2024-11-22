<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.LoginPage" %>

<%@ Register src="CaptchaImage.ascx" tagname="Captcha" tagprefix="cse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style2 {
            background-color: #000099;
        }
        .auto-style3 {
            height: 36px;
            color: #FFFFFF;
            background-color: #000099;
        }
        .auto-style1 {
            color: #FFFFFF;
        }
        #form1 {
            background-color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="LoginForm" runat="server">
        <div>
            <h1 class="auto-style3"><span class="auto-style2">Login Page</span></h1>
        </div>
        <h2>Welcome Please Login to Continue or Create a New account</h2>
        <p>
            Username:&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="btnLogin" runat="server" Text="Login" />
&nbsp;
        <br />
        <hr />
        <h2><strong>Create an Account</strong></h2>
        <p>
            Username:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </p>
        <p>
            Confirm Password:<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </p>
        <cse:Captcha runat ="server" ID="Captcha1"/>
        <p>
            <asp:Button ID="btnCreate" runat="server" Text="Create Account" />
        </p>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
