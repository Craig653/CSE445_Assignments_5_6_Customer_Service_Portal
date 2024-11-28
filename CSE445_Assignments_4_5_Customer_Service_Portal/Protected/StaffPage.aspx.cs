using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using LocalHash;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check session state to set login button
            if (Session["AccountType"] != "Staff")
            {
                ValidationLabel.Visible = true;
                ValidationLabel.Text = "User account:" + Session["Username"] + "(" + Session["AccountType"] + ")" + " does not have access to the Staff page";
            }
            else
            {
                //make panel invisible so you can't see page if you don't have correct account type
                Panel2.Visible = true;
            }


            //Set inital states and load the dashboard info
            loadStaffAccounts();
            loadAgentAccounts();
            loadMemberAccounts();
            btnlToStaff.Enabled = false;
            btnDelete.Enabled = false;
            btnToAgent.Enabled = false;
            btnToMember.Enabled = false;
            TextBox3.ReadOnly= true;
            btnNewPass.Enabled = false;
            lblAccount.Text = "";
            lblType.Text = "";

        }
        //page redirect functions
        protected void btnLoginStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffPage.aspx");
        }

        protected void btnLoginMember_Click(object sender, EventArgs e)
        {

            Response.Redirect("MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {

            Response.Redirect("AgentPage.aspx");
        }

        protected void btnTryIt_Click(object sender, EventArgs e)
        {
            Response.Redirect("../TryItPage.aspx");
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("../DefaultPage.aspx");
        }
        protected void btnComponentTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ComponentTable.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //Craig's Get username cookie on Load

            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
                Session["Username"] = null;
                Session["AccountType"] = null;
                HttpCookie delCookie = new HttpCookie("Username");
                delCookie.Expires = DateTime.Now.AddMonths(-10);
                delCookie.Value = null;
                Response.Cookies.Add(delCookie);
                HttpContext.Current.Request.Cookies.Clear();

                delCookie = new HttpCookie("Type");
                delCookie.Expires = DateTime.Now.AddMonths(-10);
                delCookie.Value = null;
                Response.Cookies.Add(delCookie);
                HttpContext.Current.Request.Cookies.Clear();

                FormsAuthentication.SignOut();
                Server.Transfer("../DefaultPage.aspx");
            }
        }


        //Get all staff accounts for the dashboard
        protected void loadStaffAccounts()
        {
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/Staff.xml");
            doc.Load(path);
            string label = "";

            foreach(XmlNode node in doc)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    label += child.FirstChild.FirstChild.Value + "<br/> ";

                }

            }
            lblStaffList.Text = label;

        }

        //Get all agent accounts for the dashboard
        protected void loadAgentAccounts()
        {
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/Agent.xml");
            doc.Load(path);
            string label = "";

            foreach (XmlNode node in doc)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    label += child.FirstChild.FirstChild.Value + "<br/> ";

                }

            }

            lblAgentList.Text = label;

        }

        //Get all member accounts for the dashboard
        protected void loadMemberAccounts()
        {
            XmlDocument doc = new XmlDocument();
            string path2 = HttpRuntime.AppDomainAppPath;
            string path = Server.MapPath("~/App_Data/Member.xml");
            doc.Load(path);
            string label = "";

            foreach (XmlNode node in doc)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    label += child.FirstChild.FirstChild.Value + "<br/> ";

                }

            }

            lblMemberList.Text = label;
        }


        //check if account exists in all xml files
        protected void btnAccount_Click(object sender, EventArgs e)
        {
            lblModifyStatus.Text = "";
            TextBox3.ReadOnly = false;
            TextBox3.Text = "";
            TextBox3.ReadOnly = true;
            lblPassword.Text = "";

            if (TextBox1.Text != "")
            {
                //look up info using xpath for all xmls
                XmlDocument docStaff = new XmlDocument();
                string path = Server.MapPath("~/App_Data/Staff.xml");
                docStaff.Load(path);
                string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" +"]";
                var myNodeStaff = docStaff.SelectSingleNode(xpath1);

                XmlDocument docAgent = new XmlDocument();;
                string path2 = Server.MapPath("~/App_Data/Agent.xml");
                docAgent.Load(path2);
                string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
                var myNodeAgent = docAgent.SelectSingleNode(xpath2);

                XmlDocument docMember = new XmlDocument();
                string path3 = Server.MapPath("~/App_Data/Member.xml");
                docMember.Load(path3);
                string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
                var myNodeMember = docMember.SelectSingleNode(xpath3);

                if(myNodeStaff != null)
                {
                    lblAccount.Text = myNodeStaff.FirstChild.InnerText;
                    lblType.Text = myNodeStaff.ParentNode.Attributes[0].InnerText;
                    btnlToStaff.Enabled = false;
                    btnDelete.Enabled = true;
                    btnToAgent.Enabled = true;
                    btnToMember.Enabled = true;
                    TextBox3.ReadOnly = false;
                    btnNewPass.Enabled = true;
                }
                else if(myNodeAgent != null)
                {
                    lblAccount.Text = myNodeAgent.FirstChild.InnerText;
                    lblType.Text = myNodeAgent.ParentNode.Attributes[0].InnerText;
                    btnlToStaff.Enabled = true;
                    btnDelete.Enabled = true;
                    btnToAgent.Enabled = false;
                    btnToMember.Enabled = true;
                    TextBox3.ReadOnly = false;
                    btnNewPass.Enabled = true;
                }
                else if(myNodeMember != null)
                {
                    lblAccount.Text = myNodeMember.FirstChild.InnerText;
                    lblType.Text = myNodeMember.ParentNode.Attributes[0].InnerText;
                    btnlToStaff.Enabled = true;
                    btnDelete.Enabled = true;
                    btnToAgent.Enabled = true;
                    btnToMember.Enabled = false;
                    TextBox3.ReadOnly = false;
                    btnNewPass.Enabled = true;
                }
                else
                {
                    lblModifyStatus.Text = "Error: Account does not Exist!";

                }


            }
        }


        //Set selected account to got to staff
        protected void btnlToStaff_Click(object sender, EventArgs e)
        {
            //look up info using xpath for all xmls
            XmlDocument docStaff = new XmlDocument();
            string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
            docStaff.Load(pathStaff);
            XmlNode rootStaff = docStaff.DocumentElement;
            string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeStaff = docStaff.SelectSingleNode(xpath1);

            XmlDocument docAgent = new XmlDocument(); ;
            string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
            docAgent.Load(pathAgent);
            XmlNode rootAgent = docAgent.DocumentElement;
            string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeAgent = docAgent.SelectSingleNode(xpath2);

            XmlDocument docMember = new XmlDocument();
            string pathMember = Server.MapPath("~/App_Data/Member.xml");
            docMember.Load(pathMember);
            XmlNode rootMember = docMember.DocumentElement;
            string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeMember = docMember.SelectSingleNode(xpath3);


            //Check if it is an agent account
            if (myNodeAgent != null)
            {

                if (myNodeAgent.ParentNode.NextSibling == null && myNodeAgent.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Cannot Move User:  " + TextBox1.Text + "  no Agents would be left!";
                }
                else
                {

                    //Create new item for staff xml file
                    XmlElement Credentials = docStaff.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Staff");
                    XmlElement Username = docStaff.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docStaff.CreateElement("Password");
                    Password.InnerText = myNodeAgent.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootStaff.AppendChild(Credentials);

                    docStaff.Save(pathStaff);


                    //remove from agent 
                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);
                    this.Page_Load(null, null);
                }

            }

            //Chekc if it is member
            else if (myNodeMember != null)
            {
                //Create new item for staff xml file
                XmlElement Credentials = docStaff.CreateElement("Credentials");
                Credentials.SetAttribute("UserType", "Staff");
                XmlElement Username = docStaff.CreateElement("Username");
                Username.InnerText = TextBox1.Text;
                XmlElement Password = docStaff.CreateElement("Password");
                Password.InnerText = myNodeMember.NextSibling.InnerText;

                Credentials.AppendChild(Username);
                Credentials.AppendChild(Password);

                rootStaff.AppendChild(Credentials);

                docStaff.Save(pathStaff);


                //remove from member
                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
                this.Page_Load(null, null);
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
            
        }

        //set selected account to agent
        protected void btnToAgent_Click(object sender, EventArgs e)
        {
            //look up info using xpath for all xmls
            XmlDocument docStaff = new XmlDocument();
            string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
            docStaff.Load(pathStaff);
            XmlNode rootStaff = docStaff.DocumentElement;
            string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeStaff = docStaff.SelectSingleNode(xpath1);

            XmlDocument docAgent = new XmlDocument(); ;
            string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
            docAgent.Load(pathAgent);
            XmlNode rootAgent = docAgent.DocumentElement;
            string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeAgent = docAgent.SelectSingleNode(xpath2);

            XmlDocument docMember = new XmlDocument();
            string pathMember = Server.MapPath("~/App_Data/Member.xml");
            docMember.Load(pathMember);
            XmlNode rootMember = docMember.DocumentElement;
            string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeMember = docMember.SelectSingleNode(xpath3);


            //check if staff
            if (myNodeStaff != null)
            {
                //validate some staff would be left!!
                if (myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Error: Cannot Move User:  " + TextBox1.Text + "  no Staff would be left!";
                }
                else
                {

                    //Create a new agent 
                    XmlElement Credentials = docAgent.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Agent");
                    XmlElement Username = docAgent.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docAgent.CreateElement("Password");
                    Password.InnerText = myNodeStaff.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootAgent.AppendChild(Credentials);

                    docAgent.Save(pathAgent);

                    //remove from staff
                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                    this.Page_Load(null, null);

                }

            }
            //check if member
            else if (myNodeMember != null)
            {

                //Create new agent
                XmlElement Credentials = docAgent.CreateElement("Credentials");
                Credentials.SetAttribute("UserType", "Agent");
                XmlElement Username = docAgent.CreateElement("Username");
                Username.InnerText = TextBox1.Text;
                XmlElement Password = docAgent.CreateElement("Password");
                Password.InnerText = myNodeMember.NextSibling.InnerText;

                Credentials.AppendChild(Username);
                Credentials.AppendChild(Password);

                rootAgent.AppendChild(Credentials);

                docAgent.Save(pathAgent);


                //Remove from member
                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
                this.Page_Load(null, null);
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
        }

        //set selected acount to member
        protected void btnToMember_Click(object sender, EventArgs e)
        {
            //look up info using xpath for all xmls
            XmlDocument docStaff = new XmlDocument();
            string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
            docStaff.Load(pathStaff);
            XmlNode rootStaff = docStaff.DocumentElement;
            string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeStaff = docStaff.SelectSingleNode(xpath1);

            XmlDocument docAgent = new XmlDocument(); ;
            string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
            docAgent.Load(pathAgent);
            XmlNode rootAgent = docAgent.DocumentElement;
            string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeAgent = docAgent.SelectSingleNode(xpath2);

            XmlDocument docMember = new XmlDocument();
            string pathMember = Server.MapPath("~/App_Data/Member.xml");
            docMember.Load(pathMember);
            XmlNode rootMember = docMember.DocumentElement;
            string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeMember = docMember.SelectSingleNode(xpath3);


            //is it staff
            if (myNodeStaff == null)
                {
                //don't let you delete only staff member
                if (myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Error: Cannot Move User:  " + TextBox1.Text + "  no Staff would be left!";
                }
                else
                {
                    //create new member
                    XmlElement Credentials = docMember.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Member");
                    XmlElement Username = docMember.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docMember.CreateElement("Password");
                    Password.InnerText = myNodeStaff.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootMember.AppendChild(Credentials);

                    docMember.Save(pathMember);

                    //remove from staff
                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                    this.Page_Load(null, null);
                }
            }
            else if (myNodeAgent != null)
            {
                if (myNodeAgent.ParentNode.NextSibling == null && myNodeAgent.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Error: Cannot Move User:  " + TextBox1.Text + "  no Agents would be left!";
                }
                else
                {
                    //create new member
                    XmlElement Credentials = docMember.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Member");
                    XmlElement Username = docMember.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docMember.CreateElement("Password");
                    Password.InnerText = myNodeAgent.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootMember.AppendChild(Credentials);

                    docMember.Save(pathMember);
                    //remove from agent
                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);
                    this.Page_Load(null, null);
                }
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
        }


        //delete account
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            //look up info using xpath for all xmls
            XmlDocument docStaff = new XmlDocument();
            string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
            docStaff.Load(pathStaff);
            XmlNode rootStaff = docStaff.DocumentElement;
            string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeStaff = docStaff.SelectSingleNode(xpath1);

            XmlDocument docAgent = new XmlDocument(); ;
            string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
            docAgent.Load(pathAgent);
            XmlNode rootAgent = docAgent.DocumentElement;
            string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeAgent = docAgent.SelectSingleNode(xpath2);

            XmlDocument docMember = new XmlDocument();
            string pathMember = Server.MapPath("~/App_Data/Member.xml");
            docMember.Load(pathMember);
            XmlNode rootMember = docMember.DocumentElement;
            string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeMember = docMember.SelectSingleNode(xpath3);

            //Check if you can acctually delete accounts, if youc an remove them from the appropriate file
            if (myNodeStaff != null)
            {
                if(myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Error: Cannot delete User:  " + TextBox1.Text + "  no Staff would be left!"; 
                }
                else
                {
                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                    this.Page_Load(null, null);
                }
            }
            else if (myNodeAgent != null)
            {
                if (myNodeAgent.ParentNode.NextSibling == null && myNodeAgent.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Error: Cannot delete User:  " + TextBox1.Text + "  no Staff would be left!";
                }
                else
                {
                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);
                    this.Page_Load(null, null);
                }
            }
            else if (myNodeMember != null)
            {

                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
                this.Page_Load(null, null);
            }
        }

        //Set new password
        protected void btnNewPass_Click(object sender, EventArgs e)
        {
            //look up info using xpath for all xmls
            XmlDocument docStaff = new XmlDocument();
            string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
            docStaff.Load(pathStaff);
            XmlNode rootStaff = docStaff.DocumentElement;
            string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeStaff = docStaff.SelectSingleNode(xpath1);

            XmlDocument docAgent = new XmlDocument(); ;
            string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
            docAgent.Load(pathAgent);
            XmlNode rootAgent = docAgent.DocumentElement;
            string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeAgent = docAgent.SelectSingleNode(xpath2);

            XmlDocument docMember = new XmlDocument();
            string pathMember = Server.MapPath("~/App_Data/Member.xml");
            docMember.Load(pathMember);
            XmlNode rootMember = docMember.DocumentElement;
            string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + TextBox1.Text + "\"" + "]";
            var myNodeMember = docMember.SelectSingleNode(xpath3);


            //Validate passowrd filed is set then encrypt and set new password
            if(TextBox3.Text.Length != 0)
            {
                if (myNodeStaff != null)
                {
                    myNodeStaff.NextSibling.InnerText = EncryptPassword(TextBox3.Text);
                    docStaff.Save(pathStaff);
                }
                else if (myNodeAgent != null)
                {
                    myNodeAgent.NextSibling.InnerText = EncryptPassword(TextBox3.Text);
                    docAgent.Save(pathAgent);
                }
                else if (myNodeMember != null)
                {
                    myNodeMember.NextSibling.InnerText = EncryptPassword(TextBox3.Text);
                    docMember.Save(pathMember);
                }
                lblPassword.Text = "Set";
                TextBox3.Text = "";
            }
            else
            {
                lblPassword.Text = "Please enter a Value";
            }



        }

        //Chris's DLL encryption
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt();
            return encryptor.EncryptString(password);
        }


        //Create new account
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if(Password1.Value == "" && txtbxUsername1.Value == "")
            {
                lblCreateStatus.Text = "Enter a Username and Password";
            }
            else if(txtbxUsername1.Value == "")
            {
                lblCreateStatus.Text = "Enter a Username";
            }
            else if (Password1.Value == "")
            {
                lblCreateStatus.Text = "Enter a Password";
            }
            else
            {
                //look up info using xpath for all xmls
                XmlDocument docStaff = new XmlDocument();
                string pathStaff = Server.MapPath("~/App_Data/Staff.xml");
                docStaff.Load(pathStaff);
                XmlNode rootStaff = docStaff.DocumentElement;
                string xpath1 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername1.Value + "\"" + "]";
                var myNodeStaff = docStaff.SelectSingleNode(xpath1);

                XmlDocument docAgent = new XmlDocument(); ;
                string pathAgent = Server.MapPath("~/App_Data/Agent.xml");
                docAgent.Load(pathAgent);
                XmlNode rootAgent = docAgent.DocumentElement;
                string xpath2 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername1.Value + "\"" + "]";
                var myNodeAgent = docAgent.SelectSingleNode(xpath2);

                XmlDocument docMember = new XmlDocument();
                string pathMember = Server.MapPath("~/App_Data/Member.xml");
                docMember.Load(pathMember);
                XmlNode rootMember = docMember.DocumentElement;
                string xpath3 = "/CredentialsDatabase/Credentials/Username[text()=\"" + txtbxUsername1.Value + "\"" + "]";
                var myNodeMember = docMember.SelectSingleNode(xpath3);

                bool testCaptcha = Captcha1.validate();


                //Validate against captacha
                if (testCaptcha)
                {
                    //If an account exists with that username accross all XMLS don't create a new one
                    if (myNodeMember == null & myNodeAgent == null && myNodeStaff == null)
                    {
                        //Figure out what type of account you want then create it based on user selection
                        if (optionsRadio1.Checked && myNodeStaff == null)
                        {

                            XmlElement Credentials = docStaff.CreateElement("Credentials");
                            Credentials.SetAttribute("UserType", "Staff");
                            XmlElement Username = docStaff.CreateElement("Username");
                            Username.InnerText = txtbxUsername1.Value;
                            XmlElement Password = docStaff.CreateElement("Password");
                            Password.InnerText = EncryptPassword(Password1.Value);

                            Credentials.AppendChild(Username);
                            Credentials.AppendChild(Password);

                            rootStaff.AppendChild(Credentials);

                            docStaff.Save(pathStaff);

                            lblCreateStatus.Text = "Staff Account: " + txtbxUsername1.Value + " created";

                        }
                        else if (optionsRadio2.Checked && myNodeAgent == null)
                        {
                            XmlElement Credentials = docAgent.CreateElement("Credentials");
                            Credentials.SetAttribute("UserType", "Agent");
                            XmlElement Username = docAgent.CreateElement("Username");
                            Username.InnerText = txtbxUsername1.Value;
                            XmlElement Password = docAgent.CreateElement("Password");
                            Password.InnerText = EncryptPassword(Password1.Value);

                            Credentials.AppendChild(Username);
                            Credentials.AppendChild(Password);

                            rootAgent.AppendChild(Credentials);

                            docAgent.Save(pathAgent);

                            lblCreateStatus.Text = "Agent Account: " + txtbxUsername1.Value + " created";
                        }
                        else if (optionsRadio3.Checked && myNodeMember == null)
                        {
                            XmlElement Credentials = docMember.CreateElement("Credentials");
                            Credentials.SetAttribute("UserType", "Member");
                            XmlElement Username = docMember.CreateElement("Username");
                            Username.InnerText = txtbxUsername1.Value;
                            XmlElement Password = docMember.CreateElement("Password");
                            Password.InnerText = EncryptPassword(Password1.Value);

                            Credentials.AppendChild(Username);
                            Credentials.AppendChild(Password);

                            rootMember.AppendChild(Credentials);

                            docMember.Save(pathMember);

                            lblCreateStatus.Text = "Member Account: " + txtbxUsername1.Value + " created";
                        }
                        //Various error checking to prevent duplicat accounts
                        else
                        {
                            if (myNodeStaff != null)
                            {
                                lblCreateStatus.Text = "Error: Staff Account: " + txtbxUsername1.Value + " already exists";
                            }
                            else if (myNodeAgent != null)
                            {
                                lblCreateStatus.Text = "Error: Agent Account: " + txtbxUsername1.Value + " already exists";
                            }
                            else if (myNodeMember != null)
                            {
                                lblCreateStatus.Text = "Error: Member Account: " + txtbxUsername1.Value + " already exists";
                            }
                        }
                    }
                    else
                    {
                        lblCreateStatus.Text = "Error: Account: " + txtbxUsername1.Value + " already exists";
                    }

                }
            }
        }
    }
}