<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaImage.ascx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.CaptchaImage" %>


<style type="text/css">

        .auto-style2 {
            font-weight: normal;
        }
    </style>

<p style="width: 534px">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="CaptchaImg" runat="server" style="height: 30px; text-align: center;" Visible="False" />
        </p>
        <p style="width: 534px">
            Type the Characters above
            <asp:TextBox ID="CaptchaEnterTxtBx" runat="server"></asp:TextBox>
            &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" style="height: 26px" Text="Validate" />
        &nbsp;<asp:Button ID="btnShowImage" runat="server" OnClick="btnShowImage_Click" Text="New Captcha" Height="26px" />
        </p>
        <p style="width: 534px">
            <span >
            <asp:Label ID="VerificationLbl" runat="server"></asp:Label>
                 </span>

        </p>
