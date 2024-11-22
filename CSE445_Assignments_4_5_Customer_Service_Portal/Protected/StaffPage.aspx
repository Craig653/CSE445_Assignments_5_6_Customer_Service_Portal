<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm4" %>

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
            <h1 class="auto-style3"><span class="auto-style1">Staff Page&nbsp;
                <asp:Button ID="lblLogout" runat="server" Text="Logout" OnClick="lblLogout_Click" style="text-align: right" />
                </span></h1>
    </form>
</body>
</html>
