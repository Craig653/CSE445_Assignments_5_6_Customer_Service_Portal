<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComponentTable.aspx.cs" Inherits="CSE445_Assignments_4_5_Customer_Service_Portal.ComponentTable" %>

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
                <a class="nav-link " id="btnLoginAgent" runat="server" onserverclick="btnLoginAgent_Click">Agent</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnLoginStaff" runat="server" onserverclick="btnLoginStaff_Click">Staff</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="btnTryIt" runat="server" onserverclick="btnTryIt_Click">TryIt Page</a>
            </li>
            <li class="nav-item ">
                  <a class="nav-link active" id="btnComponentTable" runat="server" onserverclick="btnComponentTable_Click">Component Table</a>
            </li>
            </ul>
            <div class="pull-right">
              <ul class="nav navbar-nav">
                  <li>
                    <a class="nav-link" id="Login" runat="server" onserverclick="btnLoginOut_Click">Login</a>
                  </li>
              </ul>     
            </div> 
                </div> 
        </div>
    </nav>

    <form id="form1" runat="server" style="margin: 20px;">
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

        <div class="table_component" role="region" tabindex="0">
        <table class="table table-hover">
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
                <td> Most Common Ticket Category Service</td>
                <td>
                    This component reads the ticket XML database and finds the most common category of tickets. 
                    <br><br><strong>Input:</strong> XML ticket database 
                    <br><strong>Output:</strong> Most common ticket category as a string.
                </td>
                <td>
                    It reads and processes XML data using <code>XDocument</code> 
                    to extract and count ticket categories. Used in TryItPage to display the most common category.
                </td>
            </tr>
            <tr>
                <td>Kiera Walker</td>
                <td>Global.asax Event Handlers. Used in TryIt Page</td>
                <td>
                    Tracks application lifecycle events including application start, end, and session start.
                    <br><br><strong>Input:</strong> Application lifecycle events
                    <br><strong>Output:</strong> Start/end times of application and session.
                </td>
                <td>
                    The events are captured using the Global.asax lifecycle methods 
                    (<code>Application_Start</code>, <code>Application_End</code>, <code>Session_Start</code>). 
                    The information is displayed on TryItPage.aspx for logging and diagnostic purposes.
                </td>
            </tr>
            <tr>
                <td>Kiera Walker</td>
                <td>Cookies- Automatic Login System, Used in TryIt Page </td>
                <td>
                    Enables users to create an account, which then automatically logs them in if their credentials are stored in a cookie. 
                    If credentials aren't stored, it prompts users to enter their username and password.
                    <br><br><strong>Input:</strong> Username and password
                    <br><strong>Output:</strong> Logged in status.
                </td>
                <td>
                    Credentials are saved using C# cookies (<code>HttpCookie</code>). 
                    It checks for the cookie and pre-fills username and password fields accordingly. 

                </td>
            </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-a89a02d8-7fff-a868-f22c-a170ba458566" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Cookies for table filtering</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Located on the TryIt page. </span>
                        </p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Full implementation can be viewed on the Member page.</span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                    </td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-b59f9c48-7fff-66fa-6932-15f272b6aa74" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This component sets a username cookie.&nbsp; Once a cookie is set on the try it page you can see the xml database filter. Along with viewing the filtered table on the Member page.&nbsp;</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Cookie set on Try it page. And the XML Table</span></p>
                        <br>
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">&nbsp;FilteredTree View</span></b></td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-e0c7f80b-7fff-8115-6a59-484fed390d94" style="font-weight:normal;"><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Will need to use C# Cookies, TreeView table, and Xpath&nbsp; to display the filtered XML database.</span></b></td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-b1bd8eb4-7fff-c6dd-0109-4b4c94483474" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">AI Service for Summaries</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Located on the Try it page </span>
                        </p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Generic use or the Agent page for XML database implementation</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">AskGroq (Rest)</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">ImgGroq(WSDL)</span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                    </td>
                    <td><b id="docs-internal-guid-52ebc648-7fff-3c7b-df0f-f86fff4b7225" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">The Component will look at a Customers Ticket Description and Image. It will then Use Groq AI to help describe what the problem is.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">: XML Description, and XML image data</span></p>
                        <br>
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Return: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">update the webpage with string description</span></b><meta charset="utf-8"></td>
                    <td>
                        <meta charset="utf-8">
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
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-b080ae20-7fff-e04c-4278-a79a4b99e071" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Captacha Generator&nbsp; using a ascx user control component</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Use on the Try it page</span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                    </td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-93525ec6-7fff-d67a-e2ba-8aa40ac28895" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This Service connects to ASU's string image generator. This will be used to validate people are not robots upon creating a password.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">&nbsp;String</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output: </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Label saying if string input was correct or not</span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                        <meta charset="utf-8">
                    </td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-aa4d17e6-7fff-1336-8926-9698019d8f3f" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">ASU’s Captcha generator service.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Create a User control component and insert it into the try it page and eventually the login page.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Loads image from ASUI into separate page and then pulls into the User Control component</span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                    </td>
                </tr>
                <tr>
                    <td>Craig Saunders</td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-a510091a-7fff-0b8a-6bc6-a994218e40f3" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Ticket Creator and Ticket Tool</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">See the Member page and Agent pages</span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                    </td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-954f63d6-7fff-ed54-6cea-1391da7f6ee2" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Used for Creating tickets and viewing tickets in the XML database. Also you can set the status of your tickets.<br>
                            <br>
                            </span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Inputs for Creator:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">String Description</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">JPG image &lt;25kb. See examples in Folder.</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Cookie set with username, otherwise username will be CUSTOMER</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">XML database in App_Data is updated</span></p>
                        <br>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Inputs in Ticket Tool:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">String ticket number</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Output:</span></p>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Ability to view selected ticket in labels. Connect to AI (See other service).
                            <meta charset="utf-8">
                            <b id="docs-internal-guid-adaee068-7fff-2d4c-b346-0ffb426040da" style="font-weight:normal;">Buttons to manipulate Status’s</b></span></p>
                        </b>
                        <br class="Apple-interchange-newline">
                    </td>
                    <td>
                        <meta charset="utf-8">
                        <b id="docs-internal-guid-f6fb5ec0-7fff-540a-16d6-6fa517838df0" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Access to the XML database.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Will Load the XML database into memory.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Then I can use XPath to find the correct ticket and display its corresponding v values</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">When creating a ticket XPath again will be used to search all ticket numbers. I can then find the next ticket number.&nbsp;</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">XML data will then be manipulated and saved using XmlDocument functions</span></p>
                        <br>
                        </b></td>
                </tr>
                <tr>
                    <td>Christopher Angulo</td>
                    <td>Ticket Issue Categorization based on Issue Summary<br>
                        <br>
                        WSDL<br>
                        <br>
                        Currently in TryItPage</td>
                    <td><b id="docs-internal-guid-52ebc648-7fff-3c7b-df0f-f86fff4b7226" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This component uses a service that reads the issue summary of all Tickets, and updates the Ticket Category based on the most common issues mentioned in the issue summary. Stopwords are ignored.</span></p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">: XML Element</span></p>
                        <br>
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Return: </span>X</b>ML Attribute<br>
                        <br>
                        <strong>Output:</strong> Displays Lastest Tickets XML for testing reference</td>
                    <td>Reads the XML Tickets database using XDocument, Descendants, Element, and SetAttributeValue to access specific XML Attribute contents and write to specific XML Attributes.
                        <br>
                        <br>
                        Analyzes all &lt;Text&gt; element contents, detects the most common issue, and updates such Ticket element Category with that most common issue. Ignores stopwords since these are likely not "the main issue" in a ticket.</td>
                </tr>
                <tr>
                    <td class="auto-style2">Christopher Angulo</td>
                    <td class="auto-style2">Customer Login<br>
                        <br>
                        DLL<br>
                        <br>
                        Currently in TryItPage</td>
                    <td class="auto-style2"><b id="docs-internal-guid-52ebc648-7fff-3c7b-df0f-f86fff4b7227" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This component uses a custom local library DLL </span>that uses AES encryption to encrypt user entered passwords at login (and decrypt, but decrypt not curretnly used) and compare this cyphertext to cyphertext passwords already pre-ciphered in the Credentials XML database.</p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">: Username and Password string input</span></p>
                        <br>
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Return:</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"> Successful or unsuccesful login string response</span></b></td>
                    <td class="auto-style2">Captures credential login data from user. Uses local AES encryption DLL to encrypt password, and match the key-value pair of username and password ciphertext against stored user credentials, also stored with passwords as AES ciphertext.</td>
                </tr>
                <tr>
                    <td>Christopher Angulo</td>
                    <td>Admin Login<br>
                        <br>
                        DLL<br>
                        <br>
                        Currently in TryItPage</td>
                    <td><b id="docs-internal-guid-52ebc648-7fff-3c7b-df0f-f86fff4b7228" style="font-weight:normal;">
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">This component uses a custom local library DLL </span>that uses AES encryption to encrypt user entered passwords at login (and decrypt, but decrypt not curretnly used) and compare this cyphertext to cyphertext passwords already pre-ciphered in the Credentials XML database.</p>
                        <br>
                        <p dir="ltr" style="line-height:1.2;margin-top:0pt;margin-bottom:0pt;">
                            <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Input</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:400;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">: Username and Password string input</span></p>
                        <br>
                        <span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-weight:700;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;">Return:</span><span style="font-size:11pt;font-family:'Times New Roman',serif;color:#000000;background-color:transparent;font-style:normal;font-variant:normal;text-decoration:none;vertical-align:baseline;white-space:pre;white-space:pre-wrap;"> Successful or unsuccesful login string response</span></b></td>
                    <td>Captures credential login data from user. Uses local AES encryption DLL to encrypt password, and match the key-value pair of username and password ciphertext against stored user credentials, also stored with passwords as AES ciphertext.</td>
                </tr>
            </tbody>
        </table>
        </div>
    </form>
</body>
</html>
