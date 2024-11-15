using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;


namespace CommonSearch
{
    public class TicketServiceController : ApiController
    {
        private readonly string xmlUrl = "https://github.com/Craig653/CSE445_Assignments_5_6_Customer_Service_Portal/blob/master/CSE445_Assignments_4_5_Customer_Service_Portal/App_Data/TicketsDatabase.xml";

        [HttpGet]
        [Route("api/tickets/mostcommoncategory")]
        public async Task<IHttpActionResult> GetMostCommonTicketCategory()
        {
            try
            {
                // Create an HTTP client to fetch the XML file from GitHub
                using (HttpClient client = new HttpClient())
                {
                    string xmlContent = await client.GetStringAsync(xmlUrl);

                    // Load the XML from the fetched content
                    XDocument xmlDoc = XDocument.Parse(xmlContent);

                    // Get all ticket categories
                    var categories = xmlDoc.Descendants("Ticket")
                                           .Select(ticket => (string)ticket.Attribute("Category"))
                                           .Where(category => !string.IsNullOrEmpty(category))
                                           .ToList();

                    // Find the most common category
                    var mostCommonCategory = categories.GroupBy(category => category)
                                                       .OrderByDescending(group => group.Count())
                                                       .FirstOrDefault()?.Key;

                    return Ok(mostCommonCategory ?? "No tickets available");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
