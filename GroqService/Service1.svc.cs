
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net.Http;


namespace GroqService
{
    public class Service1 : IService1
    {
        //Connect to groq via API key
        //Give it the JSON formated message and wait for response
        //Return when data is avaialable
        public async Task<string> AskGroq(string text)
        {
            //Get Authorization form groq
            string api_key = "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";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", api_key);

            string uri = "https://api.groq.com/openai/v1/chat/completions";

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(uri, requestChatJSON(text));
            string responseString = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode;


            return responseString;
        }

        //used to create Groq Json query response
        private JsonObject requestChatJSON(string text)
        {
            var json = new JsonObject
            {
                ["model"] = "mixtral-8x7b-32768",
                ["messages"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = text
                    }
                }
            };

            return json;
        }

    }

}
