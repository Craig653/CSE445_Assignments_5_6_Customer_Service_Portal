<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="AgentPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.AgentPage" %>

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
                <a class="nav-link" id="btnMemberLogin" runat="server" onserverclick="btnLoginMember_Click">Member</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" id="btnLoginAgent" runat="server" onserverclick="btnLoginAgent_Click">Agent</a>
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
        </div>
        <div class="pull-right">
            <ul class="nav navbar-nav">
                <li>
                    <a class="nav-link" id="Login" runat="server" onserverclick="btnLogout_Click">Logout</a>
                </li>
            </ul>     
        </div> 
        </div>
    </nav>

    <form id="form1" runat="server" style="margin: 20px;">
      <h2>
        <strong>Dashboard</strong>
      </h2>
      <p> Current number of Open Tickets: <asp:Label ID="lblOpenTickets" runat="server"></asp:Label>
      </p>
      <p> Current number of Closed Tickets: <asp:Label ID="lblClosedTickets" runat="server"></asp:Label>
      </p>
      <p> Current number InProgress Tickets: <asp:Label ID="lbProgressTickets" runat="server"></asp:Label>
      </p>
      <p> Most Common Category of Ticket: <asp:Label ID="lblCommonCat" runat="server"></asp:Label>
      </p>
      <hr />
      <h2><strong>Ticket Tool</strong></h2>
      <p> Enter a Ticket Number:&nbsp; <asp:TextBox ID="txtLoadTicket" runat="server" style="margin-top: 4px"></asp:TextBox> &nbsp;
        <asp:Button ID="lblLoadTicket" runat="server" OnClick="lblLoadTicket_Click" Text="Load Ticket" /> &nbsp; <asp:Label ID="lblTicketToolStatus" runat="server"></asp:Label>
      </p>
      <p>
        <strong>Ticket Number:</strong>
        <asp:Label ID="lblTicketNumber" runat="server"></asp:Label>
      </p>
      <p>
        <strong>Requester:</strong>
        <asp:Label ID="lblRequester" runat="server"></asp:Label>
      </p>
      <p>
        <strong>Ticket Description:&nbsp;</strong>
        <asp:Label ID="lblDescription" runat="server"></asp:Label>
      </p>
      <p>
        <strong>Status:</strong>
        <asp:Label ID="lblCurrentStatus" runat="server"></asp:Label>
      </p>
      <p>
        <strong>Image:</strong>
      </p>
      <p>
        <asp:Image ID="imgTicketImg" runat="server" />
      </p>
      <p> &nbsp;</p>
      <p>
        <strong>AI Tools </strong>(powered by Groq.com)
      </p>
      <p> &nbsp;
        <asp:Button ID="btnAnzDesc" runat="server" Enabled="False" Text="Analyze Description" OnClick="btnAnzDesc_Click" /> &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAnzImg" runat="server" Enabled="False" Text="Analyze Image" OnClick="btnAnzImg_Click" />
      </p>
      <p>
        <asp:Label ID="lblAIResp" runat="server"></asp:Label>
      </p>
      <p> &nbsp;</p>
      <p>
        <strong>Set Status <p> &nbsp;
            <asp:Button ID="btnSetOpen" runat="server" Enabled="False" Text="Open" Width="80px" OnClick="btnSetOpen_Click" /> &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSetClosed" runat="server" Enabled="False" Text="Closed" Width="80px" OnClick="btnSetClosed_Click" /> &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSetInProgress" runat="server" Enabled="False" Text="InProgress" Width="80px" OnClick="btnSetInProgress_Click" />
          </p>
          <p>
            <asp:Label ID="lblStatusUpdate" runat="server" style="color: #FF3300"></asp:Label>
          </p>
          <hr />
          <h2>
            <strong>Ticket Database Viewer <p> 
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
        </p>
              <p> 
                  &nbsp;</p>
    </form>
  </body>
</html>