<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.LoginPage" %>

<%@ Register src="CaptchaImage.ascx" tagname="Captcha" tagprefix="cse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, intial-scale=1, shrink-to-fit= no" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
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
                <a class="nav-link " id="btnDefault" runat="server" onserverclick="btnDefault_Click">Home</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnMemberLogin" runat="server" onserverclick="btnLoginMember_Click">Member</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnLoginAgent" runat="server" onserverclick="btnLoginAgent_Click">Agent</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnLoginStaff" runat="server" onserverclick="btnLoginStaff_Click">Staff</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnTryIt" runat="server" onserverclick="btnTryIt_Click">TryIt Page</a>
            </li>
            <li class="nav-item">
                <a class="nav-link">Component Table</a>
            </li>
            </ul>
                <div class="pull-right">
                    <ul class="nav navbar-nav">
                        <li>
                            <a class="nav-link" id="Login" runat="server" onserverclick="btnLoginBar_Click">Login</a>
                        </li>
                    </ul>     
                </div> 
            </div>
        </div>
    </nav>

    <form id="LoginForm" runat="server" style="margin: 20px;">

        <h2>Welcome Please Login </h2>

        <div>
          <label for="txtbxUsername" class="form-label mt-4 w-25">Username</label>
          <input type="text" class="form-control w-25" id="txtbxUsername" aria-describedby="txtbxUsername" placeholder="Enter Username" runat="server"/>
        </div>
        <div>
          <label for="txtbxPassword" class="form-label mt-4 w-25">Password</label>
          <input type="password" class="form-control w-25" id="txtbxPassword" placeholder="Password" autocomplete="off" runat="server"/>
        </div>
        <br />

        <button type="button" class="btn btn-primary" id="btnLogin" runat="server" onserverclick="btnLogin_Click">Login</button>
        &nbsp;
        <button type="button" class="btn btn-primary" id="btnShowCreate" runat="server" onserverclick="btnShowCreate_Click">Create New Account</button>
        <br />
        <asp:Label ID="lblAuthentication" runat="server"></asp:Label>
        <br />
        <hr />

        <asp:Panel ID="Panel1" runat="server">
            <h2><strong>Create an Account</strong></h2>
            <div>
              <label for="txtbxUsername1" class="form-label mt-4 w-25">Username</label>
              <input type="text" class="form-control w-25" id="txtbxUsername1" aria-describedby="txtbxUsername1" placeholder="Enter Username" runat="server"/>
            </div>
            <div>
              <label for="txtbxPassword" class="form-label mt-4 w-25">Password</label>
              <input type="password" class="form-control w-25" id="Password1" placeholder="Password" autocomplete="off" runat="server"/>
            </div>

            <br />

            <cse:Captcha runat ="server" ID="Captcha1"/>

            <br />

            <button type="button" class="btn btn-primary" id="btnCreate" runat="server">Create Account</button>

            <hr />

        </asp:Panel>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
