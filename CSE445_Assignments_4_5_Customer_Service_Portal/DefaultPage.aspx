<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultPage.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: #000099">
            <h1 style="height: 36px"><span class="auto-style1">Default Page&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                <asp:Button ID="btnLogin" runat="server" Text="Login" Height="28px" style="text-align: justify" Width="66px" OnClick="btnLogin_Click" />
                </span>
                <br />
            </h1>
        </div>
        <h3 style="width: 288px"><strong>Customer Service App Overview </strong></h3>
        <p>
            <strong>Main Features:</strong></p>
        <ol>
            <li>
                <p>
                    <strong>Account Creation &amp; Ticket Submission:</strong></p>
                <ul>
                    <li>Users can create accounts and submit tickets that include a problem description and images.</li>
                </ul>
            </li>
            <li>
                <p>
                    <strong>Staff Page:</strong></p>
                <ul>
                    <li>Staff can view tickets, change their statuses (e.g., &quot;Open,&quot; &quot;In Progress,&quot; &quot;Closed&quot;), and track common issues to improve service.</li>
                </ul>
            </li>
        </ol>
        <hr />
        <h3><strong>Local Component Layer Breakdown:</strong></h3>
        <ul>
            <li>
                <p>
                    <strong>Kiera</strong>:</p>
                <ul>
                    <li><strong>Global.asax file</strong>: Manages global application events (e.g., session start/end, error handling).</li>
                    <li><strong>Cookies</strong>: Handles session management and user authentication.</li>
                </ul>
            </li>
            <li>
                <p>
                    <strong>Craig</strong>:</p>
                <ul>
                    <li><strong>User Control Captcha</strong>: Ensures security and prevents bot submissions during registration.</li>
                </ul>
            </li>
            <ul>
                <li><strong>Cookies</strong>: Also handles user session data or preferences (likely in conjunction with Kiera).</li>
            </ul>
            <li>
                <p>
                    <strong>Chris</strong>:</p>
                <ul>
                    <li><strong>DLL (Dynamic Link Library) - Encryption</strong>: Provides encryption services for sensitive data, such as passwords or user information.</li>
                    <li><strong>User Control (Login Info)</strong>: Creates UI components specifically for logging in, securely handling login information, and maintaining session state.</li>
                </ul>
            </li>
        </ul>
        <hr />
        <h3>Application and Summary Table</h3>
        <style>
        .table_component {
            overflow: auto;
            width: 100%;
        }

        .table_component table {
            border: 1px solid #dededf;
            height: 100%;
            width: 100%;
            table-layout: fixed;
            border-collapse: collapse;
            border-spacing: 1px;
            text-align: left;
        }

        .table_component caption {
            caption-side: top;
            text-align: left;
        }

        .table_component th {
            border: 1px solid #dededf;
            background-color: #eceff1;
            color: #000000;
            padding: 5px;
        }

        .table_component td {
            border: 1px solid #dededf;
            background-color: #ffffff;
            color: #000000;
            padding: 5px;
        }
        </style>
        <div class="table_component" role="region" tabindex="0">
        <table>
            <thead>
                <tr>
                    <th>Provider Name</th>
                    <th>Page and component type, e.g., aspx, DLL, SVC, etc.</th>
                    <th>Component description: What does the component do? What are inputs/parameters and output/return value? </th>
                    <th>Actual resources and methods used to implement the component and where this component is used.</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Kiera Walker</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Kiera Walker</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Christopher Angulo</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Christopher Angulo</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
        </div>
    </form>
</body>
</html>
