﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.StaffPage" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <h1 class="auto-style3"><span class="auto-style1">Staff Page&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="lblLogout" runat="server" Text="Logout" />
                 </span></h1>
        </div>
        <h2><strong>Dashboard</strong></h2>
        <p>
            Current number of Open Tickets:
            <asp:Label ID="lblOpenTickets" runat="server"></asp:Label>
        </p>
        <p>
            Current number of Closed Tickets:
            <asp:Label ID="lblClosedTickets" runat="server"></asp:Label>
        </p>
        <p>
            Current number InProgress Tickets:
            <asp:Label ID="lbProgressTickets" runat="server"></asp:Label>
        </p>
        <p>
            Most Common Category of Ticket:
            <asp:Label ID="lblCommonCat" runat="server"></asp:Label>
        </p>
        <hr />
        <h2>Ticket Tool</h2>
        <p>
            Enter a Ticket Number:&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="lblLoadTicket" runat="server" Text="Load Ticket" />
        </p>
        <p>
            *Logic to check if ticket is available and display its info</p>
        <p>
            Ticket Number:
            <asp:Label ID="lblTicketNumber" runat="server"></asp:Label>
        </p>
        <p>
            Requester:
            <asp:Label ID="lblRequester" runat="server"></asp:Label>
        </p>
        <p>
            Ticket Description:&nbsp;
            <asp:Label ID="lblDescription" runat="server"></asp:Label>
        </p>
        <p>
            Status: <asp:Label ID="lblCurrentStatus" runat="server"></asp:Label>
        </p>
        <p>
            Image:</p>
        <p>
            <asp:Image ID="imgTicketImg" runat="server" />
        </p>
        <p>
            &nbsp;</p>
 &nbsp;</p>
        <p>
            <strong>AI Tools </strong>(powered by Groq.com)</p>
        <p>
&nbsp;<asp:Button ID="btnAnzDesc" runat="server" Enabled="False" Text="Analyze Description" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAnzImg" runat="server" Enabled="False" Text="Analyze Image" />
        </p>
        <p>
            <asp:Label ID="lblAIResp" runat="server"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <strong>Set Status>
        <p>
&nbsp;<asp:Bu&nbsp;<asp:Button ID="btnSetOpen" runat="server" Enabled="False" Text="Open" Width="80px" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSetClosed" runat="server" Enabled="False" Text="Closed" Width="80px" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSetInProgress" runat="server" Enabled="False" Text="InProgress" Width="80px" />
        </p>
    </form>
    <hr />
    <h2><strong>Ticket Database Viewer>
    <p>
        * Display all tickets here</p>
</body>
</html>
