using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;

namespace CommonSearch
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs when the application starts
            GlobalConfiguration.Configure(WebApiConfig.Register);
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
    }
}
