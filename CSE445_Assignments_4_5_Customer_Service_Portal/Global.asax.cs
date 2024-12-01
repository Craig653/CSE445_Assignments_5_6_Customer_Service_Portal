﻿using LocalHash;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs when the application starts
            Application["AppStartTime"] = DateTime.Now; // Store the application start time
            Trace.WriteLine("Application_Start: " + DateTime.Now);
            PreCreateMemberHashes(); // pre-create the Member password hashes
            PreCreateStaffHashes(); // pre-create the Staff password hashes
            PreCreateAdminHashes(); // pre-create the Admin password hashes
        }

        // pre-create Member password hashes
        private void PreCreateMemberHashes()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // get the local directory
            string localFile = Path.Combine(localDir, "Member.xml"); // get the local xml credentials file path
            
            if (!Directory.Exists(localDir)) // ensure the directory exists
            {
                Directory.CreateDirectory(localDir); // if not, create it
            }

            if (!File.Exists(localFile)) // ensure the file exists
            {
                File.Create(localFile).Dispose(); // if not, create the file and dispose to release the handle
            }

            XDocument xmlDoc = new XDocument( // create the Member credentials xml document
                new XElement("Member",
                    new XElement("Credentials",
                        new XElement("Username", "Chris"),
                        new XElement("Password", EncryptPassword("Chris445")),
                        new XAttribute("UserType", "Member")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Craig"),
                        new XElement("Password", EncryptPassword("Craig445")),
                        new XAttribute("UserType", "Member")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Kiera"),
                        new XElement("Password", EncryptPassword("Kiera445")),
                        new XAttribute("UserType", "Member")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Yinong"),
                        new XElement("Password", EncryptPassword("Yinong445")),
                        new XAttribute("UserType", "Member")
                    )
                )
            );
            xmlDoc.Save(localFile); // save the xml document file
        }

        // pre-create Staff password hashes
        private void PreCreateStaffHashes()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // get the local directory
            string localFile = Path.Combine(localDir, "Staff.xml"); // get the local xml credentials file path

            if (!Directory.Exists(localDir)) // ensure the directory exists
            {
                Directory.CreateDirectory(localDir); // if not, create it
            }

            if (!File.Exists(localFile)) // ensure the file exists
            {
                File.Create(localFile).Dispose(); // Create the file and dispose to release the handle
            }

            XDocument xmlDoc = new XDocument( // create the Staff credentials xml document
                new XElement("Staff",
                    new XElement("Credentials",
                        new XElement("Username", "John"),
                        new XElement("Password", EncryptPassword("John445")),
                        new XAttribute("UserType", "Staff")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Eddie"),
                        new XElement("Password", EncryptPassword("Eddie445")),
                        new XAttribute("UserType", "Staff")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Rick"),
                        new XElement("Password", EncryptPassword("Rick445")),
                        new XAttribute("UserType", "Staff")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Cassandra"),
                        new XElement("Password", EncryptPassword("Cassandra445")),
                        new XAttribute("UserType", "Staff")
                    )
                )
            );
            xmlDoc.Save(localFile); // save the xml document file
        }

        // pre-create Admin password hashes
        private void PreCreateAdminHashes()
        {
            string localDir = HttpContext.Current.Server.MapPath("~/App_Data/"); // get the local directory
            string localFile = Path.Combine(localDir, "Admin.xml"); // get the local xml credentials file path

            if (!Directory.Exists(localDir)) // ensure the directory exists
            {
                Directory.CreateDirectory(localDir); // if not, create it
            }

            if (!File.Exists(localFile)) // ensure the file exists
            {
                File.Create(localFile).Dispose(); // create the file and dispose to release the handle
            }

            XDocument xmlDoc = new XDocument( // create the Admin credentials xml document
                new XElement("Admin",
                    new XElement("Credentials",
                        new XElement("Username", "Stephanie"),
                        new XElement("Password", EncryptPassword("Stephanie445")),
                        new XAttribute("UserType", "Admin")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Carl"),
                        new XElement("Password", EncryptPassword("Carl445")),
                        new XAttribute("UserType", "Admin")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Ramon"),
                        new XElement("Password", EncryptPassword("Ramon445")),
                        new XAttribute("UserType", "Admin")
                    ),
                    new XElement("Credentials",
                        new XElement("Username", "Jonathan"),
                        new XElement("Password", EncryptPassword("Jonathan445")),
                        new XAttribute("UserType", "Admin")
                    )
                )
            );
            xmlDoc.Save(localFile); // save the xml document file
        }

        // AES encryption for passwords
        private string EncryptPassword(string password)
        {
            CredentialEncrypt encryptor = new CredentialEncrypt(); // create a new CredentialEncrypt object
            return encryptor.EncryptString(password); // return the encrypted password string
        }

        // log app termination date and time data
        protected void Application_End()
        {
            // Code that runs when the application ends
            Application["AppEndTime"] = DateTime.Now; // Store the application end time
            Trace.WriteLine("Application_End: " + DateTime.Now);
        }

        // log session start date and time data
        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session starts
            Session["SessionStartTime"] = DateTime.Now; // Store the session start time
            Trace.WriteLine("Session_Start: " + DateTime.Now);
        }

        // Existing handlers can be left here or removed if they are not used
        protected void Application_Error(object sender, EventArgs e)
        {
            // Handle global application errors (if needed)
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends
        }
    }
}