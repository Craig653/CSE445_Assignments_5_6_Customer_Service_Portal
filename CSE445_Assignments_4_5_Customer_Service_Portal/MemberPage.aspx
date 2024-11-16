﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm3" %>

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
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 class="auto-style3"><span class="auto-style1">Member Page&nbsp;
                <asp:Button ID="lblLogout" runat="server" Text="Logout" OnClick="lblLogout_Click" style="text-align: right" />
                </span></h1>
        </div>
        <h2>Create a Ticket
            <asp:Label ID="lblSubmitStatus" runat="server"></asp:Label>
        </h2>
        <p>
            <strong>Issue:</strong></p>
        <p class="auto-style4">
&nbsp;
            <asp:TextBox ID="txtIssueBox" runat="server" Height="119px" Width="339px"></asp:TextBox>
        </p>
        <p>
            Upload an image of the problem (&lt;25kb):&nbsp;&nbsp;
        </p>
        <p>
&nbsp;<asp:FileUpload ID="FileUpload2" runat="server" />
        </p>
        <p>
            &nbsp;<asp:Button ID="btnSubmitTicket" runat="server" Text="Submit Ticket" OnClick="btnSubmitTicket_Click" />
        </p>
    <hr />
    <h2>Your Tickets<h2>&nbsp;<asp:Label ID="lblNoCookie" runat="server" style="color: #FF3300"></asp:Label>
        <p>
            <strong>
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
    </form>
    </body>
</html>
