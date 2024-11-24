<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm4" %>

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
                <a class="nav-link active" id="btnLoginStaff" runat="server" onserverclick="btnLoginStaff_Click">Staff</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnTryIt" runat="server" onserverclick="btnTryIt_Click">TryIt Page</a>
            </li>
            <li class="nav-item">
                  <a class="nav-link" id="btnComponentTable" runat="server" onserverclick="btnComponentTable_Click">Component Table</a>
            </li>
            </ul>
        </div>
        <div class="pull-right">
            <ul class="nav navbar-nav">
                <li>
                    <a class="nav-link" id="Login" runat="server" onserverclick="btnTryIt_Click">Login</a>
                </li>
            </ul>     
        </div> 
        </div>
    </nav>

    <form id="form1" runat="server" style="margin: 20px;">

    </form>
</body>
</html>
