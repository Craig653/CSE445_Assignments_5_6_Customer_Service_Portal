<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm3" %>

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
                <a class="nav-link active" id="btnMemberLogin" runat="server" onserverclick="btnLoginMember_Click">Member</a>
            </li>
            <li class="nav-item">
                <a class="nav-link " id="btnLoginAgent" runat="server" onserverclick="btnLoginAgent_Click">Agent</a>
                <span class="visually-hidden">(current)</span>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnLoginStaff" runat="server" onserverclick="btnLoginStaff_Click">Staff</a>
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
        <asp:Panel ID="Panel1" runat="server" Visible="False">
        <h2><strong>Create a Ticket</strong></h2>
        <p>
            <asp:Label ID="lblSubmitStatus" runat="server" style="color: #FF3300"></asp:Label>
        </p>
        

        <div>
          <label for="txtIssueBox" class="form-label mt-4">Input Issue</label>
          <textarea class="form-control w-50" id="txtIssueBox" rows="3" style="height: 62px;" runat="server"></textarea>
        </div>

         <label for="FileUpload2" class="form-label mt-4">Upload an Image of the Issue (<25kb)</label>
         <div class="input-group mb-3 w-50">
          <asp:FileUpload ID="FileUpload2" runat="server"/>
          <button class="btn btn-primary" type="button" runat="server" id="btnSubmitTicket" onserverclick="btnSubmitTicket_Click">Submit</button>
        </div>

    <hr />

        <p><asp:Label ID="lblNoCookie" runat="server" style="color: #FF3300"></asp:Label>
        
        </p>
        <h2><strong>Your Tickets
        
        </strong>
        
        </h2>

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
        </asp:Panel>
    </form>
    </body>
</html>
