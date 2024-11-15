<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="AgentPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.StaffPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title></title>
    <style type="text/css">
      .auto-style1 {
        color: #FFFFFF;
        background-color: #000099;
      }

      .auto-style3 {
        background-color: #000099;
      }
        .auto-style4 {
            color: #FFFFFF;
        }
    </style>
  </head>
  <body>
    <form id="form1" runat="server">
      <div>
        <h1 class="auto-style3">
            <span class="auto-style4">Agent</span><span class="auto-style1"> Page&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="lblLogout" runat="server" Text="Logout" OnClick="lblLogout_Click" />
          </span>
        </h1>
      </div>
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
      <h2>Ticket Tool</h2>
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
            <asp:Label ID="lblStatusUpdate" runat="server"></asp:Label>
          </p>
          <hr />
          <h2>
            <strong>Ticket Database Viewer <p> 
            <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" ImageSet="Simple" NodeIndent="10" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" Width="367px" >
                <DataBindings>
                    <asp:TreeNodeBinding DataMember="TicketNumber" Depth="1" FormatString="TicketNumber: {0}" SelectAction="Expand" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="RequestingUsername" Depth="1" FormatString="Requester: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Text" FormatString="Description: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
                    <asp:TreeNodeBinding DataMember="Image" Depth="1" SelectAction="None" TextField="#Name" ToolTipField="#Value" />
                    <asp:TreeNodeBinding DataMember="Status" Depth="1" FormatString="Status: {0}" SelectAction="None" TextField="#InnerText" ValueField="#InnerText" />
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