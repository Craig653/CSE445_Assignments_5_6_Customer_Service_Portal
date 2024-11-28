<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm4" %>
<%@ Register src="../CaptchaImage.ascx" tagname="Captcha" tagprefix="cse" %>
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
                    <span class="visually-hidden">(current)</span>
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
                            <a class="nav-link" id="Login" runat="server" onserverclick="btnLogout_Click">Logout</a>
                        </li>
                    </ul>     
                </div> 
            </div>
        </div>
    </nav>

    <form id="form1" runat="server" style="margin: 20px;">
        <asp:Label ID="ValidationLabel" runat="server" Visible ="False" style="color: #FF0000"></asp:Label>
        <asp:Panel ID="Panel2" runat="server" Visible="False">
        <asp:Table ID="Table1" runat="server" class="table">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Staff Accounts</asp:TableHeaderCell>
                <asp:TableHeaderCell>Agent Accounts</asp:TableHeaderCell>
                <asp:TableHeaderCell>Member Accounts</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ID="C1"><asp:Label ID="lblStaffList" runat="server"></asp:Label></asp:TableCell>
                <asp:TableCell ID="C2"><asp:Label ID="lblAgentList" runat="server"></asp:Label></asp:TableCell>
                <asp:TableCell ID="C3"><asp:Label ID="lblMemberList" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />

        <hr />
        <h2>Account Modifier</h2>
        Enter Account you would like to Modify:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnAccount" runat="server" Text="Load" OnClick="btnAccount_Click" />
        &nbsp;<asp:Label ID="lblModifyStatus" runat="server" style="color: #FF3300"></asp:Label>
        <br />
        <br />
        Account Name: <asp:Label ID="lblAccount" runat="server"></asp:Label>
        <br />
        Current Account Type:
        <asp:Label ID="lblType" runat="server"></asp:Label>
        <br />
        <br />
        New Password:
        <asp:Label ID="lblPassword" runat="server" style="color: #FF3300"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox3" runat="server" TextMode="SingleLine"></asp:TextBox>
        <asp:Button ID="btnNewPass" runat="server" OnClick="btnNewPass_Click" Text="Set" />
        <br />
        <br />
        New Account Type<br />
        <asp:Button ID="btnlToStaff" runat="server" OnClick="btnlToStaff_Click" Text="Staff" />
&nbsp;<asp:Button ID="btnToAgent" runat="server" OnClick="btnToAgent_Click" Text="Agent" />
&nbsp;<asp:Button ID="btnToMember" runat="server" OnClick="btnToMember_Click" Text="Member" />
        <br />
        <br />
        Delete Account<br />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
        <br />
        <br />
                <hr />
        <asp:Panel ID="Panel1" runat="server">
            <h2><strong>Create an Account</strong></h2>
            <p>
                <asp:Label ID="lblCreateStatus" runat="server" style="color: #FF3300"></asp:Label>
            </p>
            <div>
              <label for="txtbxUsername1" class="form-label mt-4 w-25">Username</label>
              <input type="text" class="form-control w-25" id="txtbxUsername1" aria-describedby="txtbxUsername1" placeholder="Enter Username" runat="server"/>
            </div>
            <div>
              <label for="txtbxPassword" class="form-label mt-4 w-25">Password</label>
              <input type="password" class="form-control w-25" id="Password1" placeholder="Password" autocomplete="off" runat="server"/>
            </div>

            <p>Account Type</p>
             <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="optionsRadios" id="optionsRadio1" value="option1" runat="server" checked="">
            <label class="form-check-label" for="optionsRadios4">
                Staff
            </label> 
        </div> 
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="optionsRadios" id="optionsRadio2" runat="server" value="option2">
            <label class="form-check-label" for="optionsRadios5">
                Agent
            </label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="optionsRadios" id="optionsRadio3" runat="server"  value="option3">
            <label class="form-check-label" for="optionsRadios6">
                Customer
            </label>
        </div> 

            <br />
            <br />

            <cse:Captcha runat ="server" ID="Captcha1"/>

            <br />

            <button type="button" class="btn btn-primary" id="btnCreate" onserverclick="btnCreate_Click" runat="server">Create Account</button>

            <hr />

        </asp:Panel>
            </asp:Panel>
    </form>
</body>
</html>
