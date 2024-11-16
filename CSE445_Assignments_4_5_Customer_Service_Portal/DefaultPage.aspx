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
            <h1 style="height: 42px; width: 1744px;"><span class="auto-style1">Default Page&nbsp;&nbsp;&nbsp;
               
                <asp:Button ID="btnMemberLogin" runat="server" Text="Member Login" Height="33px" style="text-align: justify" Width="108px" OnClick="btnLoginMember_Click" />
                &nbsp;
               
                <asp:Button ID="btnLoginAgent" runat="server" Text="Agent Login" Height="32px" style="text-align: justify" Width="108px" OnClick="btnLoginAgent_Click" />
                &nbsp;
               
                <asp:Button ID="btnLoginStaff" runat="server" Text="Staff Login" Height="32px" style="text-align: justify" Width="108px" OnClick="btnLoginStaff_Click" />

                &nbsp;
               
                <asp:Button ID="btnTryIt" runat="server" Text="Try It Page" Height="32px" style="text-align: justify" Width="108px" OnClick="btnTryIt_Click" />

                </span>
                <br />
            </h1>
        </div>
        <h2 style="width: 493px"><strong>Customer Service App Overview </strong></h2>
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
                    Member Pag<strong>e:</strong></p>
                <ul>
                    <li>Users submit tickets that include a problem description and images of the issue</li>
                    <li>Users can see their ticket status</li>
                </ul>
            </li>
            <li>Agent Page:<ul>
                <li>Agents can view tickets in the database via a ticket tool</li>
                <li>Agents an change the status&#39;s of tickets </li>
                <li>A Dashboard will show all ticket types</li>
                </ul>
            </li>
            <li>
                <p>
                    <strong>Staff Page:</strong></p>
                <ul>
                    <li>Can delete usernames and passwords (To be added in Assignment6)</li>
                </ul>
            </li>
            <li>Try it page<ul>
                <li>A centeral&nbsp; place to try all the features and services. (See table below for all items</li>
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
                    <li><strong>Groq AI :&nbsp; </strong>Agents can use AI to analyze the submited tickets</li>
                </ul>
            </li>
            <ul>
                <li><strong>Cookies</strong>: Uses cookies to filter the Tree view of the tickets. That way on the member page you only see the logged in users tickets</li>
                <li><strong>TreeView: </strong>Can be seen on the member page and the Agent page. It shows the current tickets in the database</li>
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
            .auto-style2 {
                height: 28px;
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
                    <td class="auto-style2">Kiera Walker</td>
                    <td class="auto-style2"></td>
                    <td class="auto-style2"></td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-a89a02d8-7fff-a868-f22c-a170ba458566" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Cookies for table filtering</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Located on the TryIt page. </span>
                        </p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Full implementation can be viewed on the Member page.</span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                    </td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-b59f9c48-7fff-66fa-6932-15f272b6aa74" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This component sets a username cookie.&nbsp; Once a cookie is set on the try it page you can see the xml database filter. Along with viewing the filtered table on the Member page.&nbsp;</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Cookie set on Try it page. And the XML Table</span></p>
                        <br />
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">&nbsp;FilteredTree View</span></b></td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-e0c7f80b-7fff-8115-6a59-484fed390d94" style="font-weight:normal;"><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Will need to use C# Cookies, TreeView table, and Xpath&nbsp; to display the filtered XML database.</span></b></td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-b1bd8eb4-7fff-c6dd-0109-4b4c94483474" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">AI Service for Summaries</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Located on the Try it page </span>
                        </p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Generic use or the Agent page for XML database implementation</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">AskGroq (Rest)</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">ImgGroq(WSDL)</span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                    </td>
                    <td><b id="docs-internal-guid-52ebc648-7fff-3c7b-df0f-f86fff4b7225" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">The Component will look at a Customers Ticket Description and Image. It will then Use Groq AI to help describe what the problem is.</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">: XML Description, and XML image data</span></p>
                        <br />
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Return: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">update the webpage with string description</span></b><meta charset="utf-8" /></td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-94852a19-7fff-62fa-1828-2231291b7c84" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Will need an API key from Groq</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">&nbsp;</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Need the proper json request format to interface with groq systems</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">&nbsp;</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Use of C# JSON and HTTP libraries</span></p>
                        </b></td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-b080ae20-7fff-e04c-4278-a79a4b99e071" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Captacha Generator&nbsp; using a ascx user control component</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Use on the Try it page</span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                    </td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-93525ec6-7fff-d67a-e2ba-8aa40ac28895" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This Service connects to ASU&#39;s string image generator. This will be used to validate people are not robots upon creating a password.</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">&nbsp;String</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Label saying if string input was correct or not</span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                        <meta charset="utf-8" />
                    </td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-aa4d17e6-7fff-1336-8926-9698019d8f3f" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">ASU’s Captcha generator service.</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Create a User control component and insert it into the try it page and eventually the login page.</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Loads image from ASUI into separate page and then pulls into the User Control component</span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                    </td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-a510091a-7fff-0b8a-6bc6-a994218e40f3" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Ticket Creator and Ticket Tool</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">See the Member page and Agent pages</span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                    </td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-954f63d6-7fff-ed54-6cea-1391da7f6ee2" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Used for Creating tickets and viewing tickets in the XML database. Also you can set the status of your tickets.<br />
                            <br />
                            </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Inputs for Creator:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">String Description</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">JPG image &lt;25kb. See examples in Folder.</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Cookie set with username, otherwise username will be CUSTOMER</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">XML database in App_Data is updated</span></p>
                        <br />
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Inputs in Ticket Tool:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">String ticket number</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Ability to view selected ticket in labels. Connect to AI (See other service).
                            <meta charset="utf-8" />
                            <b id="docs-internal-guid-adaee068-7fff-2d4c-b346-0ffb426040da" style="font-weight:normal;">Buttons to manipulate Status’s</b></span></p>
                        </b>
                        <br class="Apple-interchange-newline" />
                    </td>
                    <td>
                        <meta charset="utf-8" />
                        <b id="docs-internal-guid-f6fb5ec0-7fff-540a-16d6-6fa517838df0" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Access to the XML database.</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Will Load the XML database into memory.</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Then I can use XPath to find the correct ticket and display its corresponding v values</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">When creating a ticket XPath again will be used to search all ticket numbers. I can then find the next ticket number.&nbsp;</span></p>
                        <br />
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">XML data will then be manipulated and saved using XmlDocument functions</span></p>
                        <br />
                        </b></td>
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
