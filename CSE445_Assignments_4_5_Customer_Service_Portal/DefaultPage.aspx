<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang ="en">
<head runat="server">
    <meta name="viewport" content="width=device-width, intial-scale=1, shrink-to-fit= no" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <title></title>
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
                    <a class="nav-link active" id="btnDefault" runat="server" onserverclick="btnDefault_Click">Home</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="btnMemberLogin" runat="server" visible="false" onserverclick="btnLoginMember_Click">Member</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="btnLoginAgent" runat="server"  visible="false" onserverclick="btnLoginAgent_Click">Agent</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="btnLoginStaff" runat="server"  visible="false" onserverclick="btnLoginStaff_Click">Staff</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="btnTryIt" runat="server" onserverclick="btnTryIt_Click">TryIt Page</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="btnComponentTable" runat="server" onserverclick="btnComponentTable_Click">Component Table</a>
                </li>
              </ul>

            <div class="pull-right">
              <ul class="nav navbar-nav">
                  <li>
                    <a class="nav-link" id="Login" runat="server" onserverclick="btnLoginOut_Click">Login</a>
                  </li>
              </ul>     
            </div> 
                </div> 
          </div>
        </nav>
        <form id="form1" runat="server" style="margin: 20px;">
        <h2 ><strong>Welcome to the Customer Service App</strong></h2>

        <p>
            <strong>Summary</strong></p>
        <p>
            This web application was designed to facititate customers having issues. Customers will be able to log into the member page and submit a ticket and a picture explaining their problem.</p>
        <p>
            Agents will then be able to log in and see how many open tickets, closed tickets, inprogress tickets there are. They will also be able to work with AI to analyze the issue and images. Along with updating the ticket status and seeing a tree view of all tickets</p>
        <p>
            Staff or Admin will be able to log into the Staff page and delete or create accounts (comming in assignment 6)</p>
        <p>
            <strong>Main Pages</strong></p>
        <ol>
            <li>
                <p>
                    Member Page: </p>
                <ul>
                    <li>Users submit tickets that include a problem description and images of the issue</li>
                    <li>Users can see their ticket status</li>
                    <li>
                        <asp:Button ID="btnToMember" runat="server" OnClick="btnToMember_Click" Text="To Member Page" />
                    </li>
                </ul>
            </li>
            <li>Agent Page:<ul>
                <li>Agents can view tickets in the database via a ticket tool</li>
                <li>Agents an change the status&#39;s of tickets </li>
                <li>A Dashboard will show all ticket types</li>
                <li>
                    <asp:Button ID="btnToAgentPage" runat="server" OnClick="btnToAgentPage_Click" Text="To Agent Page" />
                </li>
                </ul>
            </li>
            <li>
                <p>
                    Staff Page:</p>
                <ul>
                    <li>Can delete usernames and passwords (To be added in Assignment6)</li>
                    <li>
                        <asp:Button ID="btnToStaffPage" runat="server" OnClick="btnToStaffPage_Click" Text="To Staff Page" />
                    </li>
                </ul>
            </li>
            <li>Try it page<ul>
                <li>A centeral&nbsp; place to try all the features and services. (See table below for all items</li>
                </ul>
            </li>
        </ol>
        <hr />
        
    </form>
</body>
</html>
