using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Web.Hosting;
using System.Web;

namespace CommonCategorySearch
{
    public class Service1 : IService1
    {
        public string GetMostCommonCategory()
        {
            try
            {
                // Load the XML document from the App_Data folder
                //string filePath = HostingEnvironment.MapPath("~/App_Data/TicketsDatabase.xml");
                string filePath = Path.Combine(HttpRuntime.AppDomainAppPath);
                filePath = filePath.Replace("CommonCategorySearch", "CSE445_Assignments_4_5_Customer_Service_Portal") + "/App_Data/TicketsDatabase.xml";

                // Load the XML document using XDocument
                XDocument xmlDoc = XDocument.Load(filePath);

                // Extract all "Category" attributes from "Ticket" elements
                var categories = xmlDoc.Descendants("Ticket")
                                       .Attributes("Category")
                                       .Select(x => x.Value)
                                       .ToList();

                // Find the most common category
                string mostCommonCategory = categories.GroupBy(c => c)
                                                      .OrderByDescending(g => g.Count())
                                                      .FirstOrDefault()?.Key;

                // Return the most common category or a default message if none found
                return mostCommonCategory ?? "No categories found.";
            }
            catch (Exception ex)
            {
                // Log any errors that occur and return an error message
                return $"Error: {ex.Message}";
            }
        }
    }
}
