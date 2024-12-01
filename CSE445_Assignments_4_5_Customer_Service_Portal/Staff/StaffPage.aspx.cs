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

            //Only show buttons users have access to
            if (Session["AccountType"] != null)
            {
                if (Session["AccountType"] == "Staff")
                {
                    btnLoginStaff.Visible = true;
                }
                else if (Session["AccountType"] == "Agent")
                {
                    btnLoginAgent.Visible = true;
                }
                else if (Session["AccountType"] == "Member")
                {
                    btnMemberLogin.Visible = true;
                }
            }

            //Check session state to set login button
            if (Session["AccountType"] != "Staff")
            {
                ValidationLabel.Visible = true;
                ValidationLabel.Text = "User account:" + Session["Username"] + "(" + Session["AccountType"] + ")" + " does not have access to the Staff page";
            }
            else
            {
                //make panel invisible so you can't see page if you don't have correct account type
                panel2.Visible = true;
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

            Response.Redirect("../Member/MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Agent/AgentPage.aspx");
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

        private void userLogout()
        {
            //Craig's Get username cookie on Load

            HttpCookie userCookie = Request.Cookies["Username"];
            if ((userCookie != null))
            {
                Session["Username"] = null;
                Session["AccountType"] = null;
                Session["buttonID"] = null;
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            userLogout();
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


                if (myNodeStaff != null)
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

            string xpath4 = "/configuration/system.web/authorization";
            string xpath5 = "/configuration/system.web/authorization/deny";
            string xpath6 = "/configuration/system.web/authorization/allow[@users=\"" + TextBox1.Text + "\"" + "]";

            XmlDocument docMemberWeb = new XmlDocument();
            string pathMemberWeb = Server.MapPath("~/Member/web.config");
            docMemberWeb.Load(pathMemberWeb);
            var myNodeMemberWeb = docMemberWeb.SelectSingleNode(xpath4);
            var myNodeMemberWebRef = docMemberWeb.SelectSingleNode(xpath5);
            var myNodeMemberRem = docMemberWeb.SelectSingleNode(xpath6);

            XmlDocument docAgentWeb = new XmlDocument();
            string pathAgentWeb = Server.MapPath("~/Agent/web.config");
            docAgentWeb.Load(pathAgentWeb);
            var myNodeAgentWeb = docAgentWeb.SelectSingleNode(xpath4);
            var myNodeAgentWebRef = docAgentWeb.SelectSingleNode(xpath5);
            var myNodeAgentRem = docAgentWeb.SelectSingleNode(xpath6);

            XmlDocument docStaffWeb = new XmlDocument();
            string pathStaffWeb = Server.MapPath("~/Staff/web.config");
            docStaffWeb.Load(pathStaffWeb);
            var myNodeStaffWeb = docStaffWeb.SelectSingleNode(xpath4);
            var myNodeStaffWebRef = docStaffWeb.SelectSingleNode(xpath5);
            var myNodeStaffRem = docStaffWeb.SelectSingleNode(xpath6);

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

                    //update Staff webconfig to allow user as a Staff
                    XmlElement WebUser = docStaffWeb.CreateElement("allow");
                    WebUser.SetAttribute("users", TextBox1.Text);
                    myNodeStaffWeb.PrependChild(WebUser);
                    docStaffWeb.Save(pathStaffWeb);

                    //Remove from Agetn web.config
                    docAgentWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeAgentRem);
                    docAgentWeb.Save(pathAgentWeb);

                    //remove from agent 
                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);

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

                //update Staff webconfig to allow user as a Staff
                XmlElement WebUser = docStaffWeb.CreateElement("allow");
                WebUser.SetAttribute("users", TextBox1.Text);
                myNodeStaffWeb.PrependChild(WebUser);
                docStaffWeb.Save(pathStaffWeb);

                //Remove from Member web.config
                docMemberWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeMemberRem);
                docMemberWeb.Save(pathMemberWeb);


                //remove from member
                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
                this.Page_Load(null, null);

            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
            userLogout();
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

            string xpath4 = "/configuration/system.web/authorization";
            string xpath5 = "/configuration/system.web/authorization/deny";
            string xpath6 = "/configuration/system.web/authorization/allow[@users=\"" + TextBox1.Text + "\"" + "]";

            XmlDocument docMemberWeb = new XmlDocument();
            string pathMemberWeb = Server.MapPath("~/Member/web.config");
            docMemberWeb.Load(pathMemberWeb);
            var myNodeMemberWeb = docMemberWeb.SelectSingleNode(xpath4);
            var myNodeMemberWebRef = docMemberWeb.SelectSingleNode(xpath5);
            var myNodeMemberRem = docMemberWeb.SelectSingleNode(xpath6);

            XmlDocument docAgentWeb = new XmlDocument();
            string pathAgentWeb = Server.MapPath("~/Agent/web.config");
            docAgentWeb.Load(pathAgentWeb);
            var myNodeAgentWeb = docAgentWeb.SelectSingleNode(xpath4);
            var myNodeAgentWebRef = docAgentWeb.SelectSingleNode(xpath5);
            var myNodeAgentRem = docAgentWeb.SelectSingleNode(xpath6);

            XmlDocument docStaffWeb = new XmlDocument();
            string pathStaffWeb = Server.MapPath("~/Staff/web.config");
            docStaffWeb.Load(pathStaffWeb);
            var myNodeStaffWeb = docStaffWeb.SelectSingleNode(xpath4);
            var myNodeStaffWebRef = docStaffWeb.SelectSingleNode(xpath5);
            var myNodeStaffRem = docStaffWeb.SelectSingleNode(xpath6);

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

                    //update webconfig to allow user as a Agent
                    XmlElement WebUser = docAgentWeb.CreateElement("allow");
                    WebUser.SetAttribute("users", TextBox1.Text);
                    myNodeAgentWeb.PrependChild(WebUser);
                    docAgentWeb.Save(pathAgentWeb);

                    //Remove from Staff web.config
                    docStaffWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeStaffRem);
                    docStaffWeb.Save(pathStaffWeb);

                    //remove from staff
                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);

                }
                userLogout();

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

                //update webconfig to allow user as a Agent
                XmlElement WebUser = docAgentWeb.CreateElement("allow");
                WebUser.SetAttribute("users", TextBox1.Text);
                myNodeAgentWeb.PrependChild(WebUser);
                docAgentWeb.Save(pathAgentWeb);

                //Remove from Member web.config
                docMemberWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeMemberRem);
                docMemberWeb.Save(pathMemberWeb);


                //Remove from member
                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
            this.Page_Load(null, null);
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

            string xpath4 = "/configuration/system.web/authorization";
            string xpath5 = "/configuration/system.web/authorization/deny";
            string xpath6 = "/configuration/system.web/authorization/allow[@users=\"" + TextBox1.Text + "\"" + "]";

            XmlDocument docMemberWeb = new XmlDocument();
            string pathMemberWeb = Server.MapPath("~/Member/web.config");
            docMemberWeb.Load(pathMemberWeb);
            var myNodeMemberWeb = docMemberWeb.SelectSingleNode(xpath4);
            var myNodeMemberWebRef = docMemberWeb.SelectSingleNode(xpath5);
            var myNodeMemberRem = docMemberWeb.SelectSingleNode(xpath6);

            XmlDocument docAgentWeb = new XmlDocument();
            string pathAgentWeb = Server.MapPath("~/Agent/web.config");
            docAgentWeb.Load(pathAgentWeb);
            var myNodeAgentWeb = docAgentWeb.SelectSingleNode(xpath4);
            var myNodeAgentWebRef = docAgentWeb.SelectSingleNode(xpath5);
            var myNodeAgentRem = docAgentWeb.SelectSingleNode(xpath6);

            XmlDocument docStaffWeb = new XmlDocument();
            string pathStaffWeb = Server.MapPath("~/Staff/web.config");
            docStaffWeb.Load(pathStaffWeb);
            var myNodeStaffWeb = docStaffWeb.SelectSingleNode(xpath4);
            var myNodeStaffWebRef = docStaffWeb.SelectSingleNode(xpath5);
            var myNodeStaffRem = docStaffWeb.SelectSingleNode(xpath6);

            //is it staff
            if (myNodeStaff != null)
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

                    //update webconfig to allow user as a member
                    XmlElement WebUser = docMemberWeb.CreateElement("allow");
                    WebUser.SetAttribute("users", TextBox1.Text);
                    myNodeMemberWeb.PrependChild(WebUser);
                    docMemberWeb.Save(pathMemberWeb);

                    //Remove from Staff web.config
                    docStaffWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeStaffRem);
                    docStaffWeb.Save(pathStaffWeb);

                    //remove from staff
                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                }
                userLogout();
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

                    //update webconfig to allow user as a member
                    XmlElement WebUser = docMemberWeb.CreateElement("allow");
                    WebUser.SetAttribute("users", TextBox1.Text);
                    myNodeMemberWeb.PrependChild(WebUser);
                    docMemberWeb.Save(pathMemberWeb);

                    //Remove from Member web.config
                    docAgentWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeAgentRem);
                    docAgentWeb.Save(pathAgentWeb);

                    docMember.Save(pathMember);
                    //remove from agent
                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);
                }
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
            this.Page_Load(null, null);

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

            string xpath4 = "/configuration/system.web/authorization";
            string xpath5 = "/configuration/system.web/authorization/deny";
            string xpath6 = "/configuration/system.web/authorization/allow[@users=\"" + TextBox1.Text + "\"" + "]";

            XmlDocument docMemberWeb = new XmlDocument();
            string pathMemberWeb = Server.MapPath("~/Member/web.config");
            docMemberWeb.Load(pathMemberWeb);
            var myNodeMemberWeb = docMemberWeb.SelectSingleNode(xpath4);
            var myNodeMemberWebRef = docMemberWeb.SelectSingleNode(xpath5);
            var myNodeMemberRem = docMemberWeb.SelectSingleNode(xpath6);

            XmlDocument docAgentWeb = new XmlDocument();
            string pathAgentWeb = Server.MapPath("~/Agent/web.config");
            docAgentWeb.Load(pathAgentWeb);
            var myNodeAgentWeb = docAgentWeb.SelectSingleNode(xpath4);
            var myNodeAgentWebRef = docAgentWeb.SelectSingleNode(xpath5);
            var myNodeAgentRem = docAgentWeb.SelectSingleNode(xpath6);

            XmlDocument docStaffWeb = new XmlDocument();
            string pathStaffWeb = Server.MapPath("~/Staff/web.config");
            docStaffWeb.Load(pathStaffWeb);
            var myNodeStaffWeb = docStaffWeb.SelectSingleNode(xpath4);
            var myNodeStaffWebRef = docStaffWeb.SelectSingleNode(xpath5);
            var myNodeStaffRem = docStaffWeb.SelectSingleNode(xpath6);

            //Check if you can acctually delete accounts, if youc an remove them from the appropriate file
            if (myNodeStaff != null)
            {
                if(myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Error: Cannot delete User:  " + TextBox1.Text + "  no Staff would be left!"; 
                }
                else
                {

                    //Remove from Staff web.config
                    docStaffWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeStaffRem);
                    docStaffWeb.Save(pathStaffWeb);

                    //Remove from staff list
                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                    userLogout();
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
                    //Remove from Agent web.config
                    docAgentWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeAgentRem);
                    docAgentWeb.Save(pathAgentWeb);

                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);
                }
                this.Page_Load(null, null);
            }
            else if (myNodeMember != null)
            {
                //Remove from Member web.config
                docMemberWeb.ChildNodes[1].ChildNodes[0].ChildNodes[0].RemoveChild(myNodeMemberRem);
                docMemberWeb.Save(pathMemberWeb);

                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
            }
            this.Page_Load(null, null);

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

            this.Page_Load(null, null);

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

                string xpath4 = "/configuration/system.web/authorization";
                string xpath5 = "/configuration/system.web/authorization/deny";
                string xpath6 = "/configuration/system.web/authorization/allow[@users=\"" + txtbxUsername1.Value + "\"" + "]";

                XmlDocument docMemberWeb = new XmlDocument();
                string pathMemberWeb = Server.MapPath("~/Member/web.config");
                docMemberWeb.Load(pathMemberWeb);
                var myNodeMemberWeb = docMemberWeb.SelectSingleNode(xpath4);
                var myNodeMemberWebRef = docMemberWeb.SelectSingleNode(xpath5);
                var myNodeMemberRem = docMemberWeb.SelectSingleNode(xpath6);

                XmlDocument docAgentWeb = new XmlDocument();
                string pathAgentWeb = Server.MapPath("~/Agent/web.config");
                docAgentWeb.Load(pathAgentWeb);
                var myNodeAgentWeb = docAgentWeb.SelectSingleNode(xpath4);
                var myNodeAgentWebRef = docAgentWeb.SelectSingleNode(xpath5);
                var myNodeAgentRem = docAgentWeb.SelectSingleNode(xpath6);

                XmlDocument docStaffWeb = new XmlDocument();
                string pathStaffWeb = Server.MapPath("~/Staff/web.config");
                docStaffWeb.Load(pathStaffWeb);
                var myNodeStaffWeb = docStaffWeb.SelectSingleNode(xpath4);
                var myNodeStaffWebRef = docStaffWeb.SelectSingleNode(xpath5);
                var myNodeStaffRem = docStaffWeb.SelectSingleNode(xpath6);

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

                            //update webconfig to allow user as a Staff
                            XmlElement WebUser = docStaffWeb.CreateElement("allow");
                            WebUser.SetAttribute("users", txtbxUsername1.Value);
                            myNodeStaffWeb.PrependChild(WebUser);
                            docStaffWeb.Save(pathStaffWeb);

                            lblCreateStatus.Text = "Staff Account: " + txtbxUsername1.Value + " created";

                            userLogout();

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

                            //update webconfig to allow user as a Agent
                            XmlElement WebUser = docAgentWeb.CreateElement("allow");
                            WebUser.SetAttribute("users", txtbxUsername1.Value);
                            myNodeAgentWeb.PrependChild(WebUser);
                            docAgentWeb.Save(pathAgentWeb);

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

                            //update webconfig to allow user as a member
                            XmlElement WebUser = docMemberWeb.CreateElement("allow");
                            WebUser.SetAttribute("users", txtbxUsername1.Value);
                            myNodeMemberWeb.PrependChild(WebUser);
                            docMemberWeb.Save(pathMemberWeb);

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
                    this.Page_Load(null, null);
                }

            }
        }
    }
}