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
        #form1 {
            background-color: #FFFFFF;
        }
        #LoginForm {
            background-color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="LoginForm" runat="server">
        <div>
            <h1 class="auto-style3"><span class="auto-style2">Login Page&nbsp;&nbsp;&nbsp; <asp:Button ID="btnDefault" runat="server" OnClick="btnDefault_Click" Text="Default Page" />
                </span></h1>
        </div>
        <h2>Welcome Please Login </h2>
        <p>
            Username:&nbsp;
            <asp:TextBox ID="txtbxUsername" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:
            <asp:TextBox ID="txtbxPassword" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
&nbsp;
        <asp:Button ID="btnShowCreate" runat="server" OnClick="btnShowCreate_Click" Text="Create New Account" />
        <br />
        <asp:Label ID="lblAuthentication" runat="server"></asp:Label>
        <br />
        <hr />

        <asp:Panel ID="Panel1" runat="server">
            <h2><strong>Create an Account</strong></h2>
            <p>
                Username:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </p>
            <p>
                Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </p>
            <p>
                Confirm Password:&nbsp;
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </p>
            <cse:Captcha runat ="server" ID="Captcha1"/>
            <p>
                <asp:Button ID="btnCreate" runat="server" Text="Create Account" />
            </p>
            <hr />
            <p>
                &nbsp;</p>
        </asp:Panel>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
