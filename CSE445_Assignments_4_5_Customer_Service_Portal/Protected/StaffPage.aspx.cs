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
            loadStaffAccounts();
            loadAgentAccounts();
            loadMemberAccounts();
            btnlToStaff.Enabled = false;
            btnDelete.Enabled = false;
            btnToAgent.Enabled = false;
            btnToMember.Enabled = false;
            TextBox2.Enabled = false;
            btnNewPass.Enabled = false;
            TextBox2.Text = "";
            lblAccount.Text = "";
            lblType.Text = "";

        }
        protected void btnLoginStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffPage.aspx");
        }

        protected void btnLoginMember_Click(object sender, EventArgs e)
        {
            //check login cookies and database
            //Server.Transfer("LoginPage.aspx");
            Response.Redirect("MemberPage.aspx");
        }

        protected void btnLoginAgent_Click(object sender, EventArgs e)
        {
            //check login cookies and database
            //Server.Transfer("LoginPage.aspx");
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

        protected void btnAccount_Click(object sender, EventArgs e)
        {
            lblModifyStatus.Text = "";
            TextBox2.Text = "";
            lblPassword.Text = "";

            if (TextBox1.Text != "")
            {
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
                    TextBox2.Enabled = true;
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
                    TextBox2.Enabled = true;
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
                    TextBox2.Enabled = true;
                    btnNewPass.Enabled = true;
                }
                else
                {
                    lblModifyStatus.Text = "Account does not Exist!";

                }


            }
        }

        protected void btnlToStaff_Click(object sender, EventArgs e)
        {

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

            if (myNodeAgent != null)
            {

                if (myNodeAgent.ParentNode.NextSibling == null && myNodeAgent.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Cannot Move User:  " + TextBox1.Text + "  no Agents would be left!";
                }
                else
                {


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

                    rootAgent.RemoveChild(myNodeAgent.ParentNode);
                    docAgent.Save(pathAgent);
                    this.Page_Load(null, null);
                }

            }
            else if (myNodeMember != null)
            {
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

                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
                this.Page_Load(null, null);
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
        }

        protected void btnToAgent_Click(object sender, EventArgs e)
        {

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

            if (myNodeStaff != null)
            {
                if (myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Cannot Move User:  " + TextBox1.Text + "  no Staff would be left!";
                }
                else
                {

                    XmlElement Credentials = docAgent.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Staff");
                    XmlElement Username = docAgent.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docAgent.CreateElement("Password");
                    Password.InnerText = myNodeStaff.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootAgent.AppendChild(Credentials);

                    docAgent.Save(pathAgent);

                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                    this.Page_Load(null, null);

                }

            }
            else if (myNodeMember != null)
            {
                XmlElement Credentials = docAgent.CreateElement("Credentials");
                Credentials.SetAttribute("UserType", "Staff");
                XmlElement Username = docAgent.CreateElement("Username");
                Username.InnerText = TextBox1.Text;
                XmlElement Password = docAgent.CreateElement("Password");
                Password.InnerText = myNodeMember.NextSibling.InnerText;

                Credentials.AppendChild(Username);
                Credentials.AppendChild(Password);

                rootAgent.AppendChild(Credentials);

                docAgent.Save(pathAgent);

                rootMember.RemoveChild(myNodeMember.ParentNode);
                docMember.Save(pathMember);
                this.Page_Load(null, null);
            }
            else
            {
                lblModifyStatus.Text = "Well this is awkward.... Logout and try again";

            }
        }

        protected void btnToMember_Click(object sender, EventArgs e)
        {
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

            if (myNodeStaff != null)
            {
                if (myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Cannot Move User:  " + TextBox1.Text + "  no Staff would be left!";
                }
                else
                {
                    XmlElement Credentials = docMember.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Staff");
                    XmlElement Username = docMember.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docMember.CreateElement("Password");
                    Password.InnerText = myNodeStaff.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootMember.AppendChild(Credentials);

                    docMember.Save(pathMember);

                    rootStaff.RemoveChild(myNodeStaff.ParentNode);
                    docStaff.Save(pathStaff);
                    this.Page_Load(null, null);
                }
            }
            else if (myNodeAgent != null)
            {
                if (myNodeAgent.ParentNode.NextSibling == null && myNodeAgent.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Cannot Move User:  " + TextBox1.Text + "  no Agents would be left!";
                }
                else
                {
                    XmlElement Credentials = docMember.CreateElement("Credentials");
                    Credentials.SetAttribute("UserType", "Staff");
                    XmlElement Username = docMember.CreateElement("Username");
                    Username.InnerText = TextBox1.Text;
                    XmlElement Password = docMember.CreateElement("Password");
                    Password.InnerText = myNodeAgent.NextSibling.InnerText;

                    Credentials.AppendChild(Username);
                    Credentials.AppendChild(Password);

                    rootMember.AppendChild(Credentials);

                    docMember.Save(pathMember);

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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
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

            if (myNodeStaff != null)
            {
                if(myNodeStaff.ParentNode.NextSibling == null && myNodeStaff.ParentNode.PreviousSibling == null)
                {
                    lblModifyStatus.Text = "Cannot delete User:  " + TextBox1.Text + "  no Staff would be left!"; 
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
                    lblModifyStatus.Text = "Cannot delete User:  " + TextBox1.Text + "  no Staff would be left!";
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

        protected void btnNewPass_Click(object sender, EventArgs e)
        {
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

            if (myNodeStaff != null)
            {
                myNodeStaff.NextSibling.InnerText = EncryptPassword(TextBox2.Text);
                docStaff.Save(pathStaff);
            }
            else if (myNodeAgent != null)
            {
                myNodeAgent.NextSibling.InnerText = EncryptPassword(TextBox2.Text);
                docAgent.Save(pathAgent);
            }
            else if (myNodeMember != null)
            {
                myNodeMember.NextSibling.InnerText = EncryptPassword(TextBox2.Text);
                docMember.Save(pathMember);
            }

            lblPassword.Text = TextBox2.Text;
            TextBox2.Text = "";
        }

        //Chris's DLL encryption
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt();
            return encryptor.EncryptString(password);
        }
    }
}