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
        // URL to the raw XML data file on GitHub
        private readonly string xmlUrl = "https://raw.githubusercontent.com/Craig653/CSE445_Assignments_5_6_Customer_Service_Portal/master/CSE445_Assignments_4_5_Customer_Service_Portal/App_Data/TicketsDatabase.xml";

        // GET api/tickets/mostcommoncategory
        [HttpGet]
        [Route("DefaultPage.aspx")]
        public async Task<IHttpActionResult> GetMostCommonTicketCategory()
        {
            try
            {
                // Create an HTTP client to fetch the XML file from GitHub
                using (HttpClient client = new HttpClient())
                {
                    // Fetch the XML file content from the GitHub raw URL
                    string xmlContent = await client.GetStringAsync(xmlUrl);

                    // Load the XML from the fetched content
                    XDocument xmlDoc = XDocument.Parse(xmlContent);

                    // Extract all ticket categories
                    var categories = xmlDoc.Descendants("Ticket")
                                           .Select(ticket => (string)ticket.Attribute("Category"))
                                           .Where(category => !string.IsNullOrEmpty(category))
                                           .ToList();

                    // Find the most common category
                    var mostCommonCategory = categories.GroupBy(category => category)
                                                       .OrderByDescending(group => group.Count())
                                                       .FirstOrDefault()?.Key;

                    // Return the result
                    return Ok(mostCommonCategory ?? "No tickets available");
                }
            }
            catch (HttpRequestException httpRequestEx)
            {
                // Handle network-related errors, e.g., GitHub is unreachable
                return Content(HttpStatusCode.BadRequest, $"Error fetching data from GitHub: {httpRequestEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle general errors
                return InternalServerError(ex);
            }
        }
    }
}
