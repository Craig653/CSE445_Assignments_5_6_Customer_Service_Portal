﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs when the application starts
            Application["AppStartTime"] = DateTime.Now; // Store the application start time
            Trace.WriteLine("Application_Start: " + DateTime.Now);
        }

        protected void Application_End()
        {
            // Code that runs when the application ends
            Application["AppEndTime"] = DateTime.Now; // Store the application end time
            Trace.WriteLine("Application_End: " + DateTime.Now);
        }

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