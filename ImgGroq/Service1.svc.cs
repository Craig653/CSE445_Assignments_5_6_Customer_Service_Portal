using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ImgGroq
{
    public class Service1 : IService1
    {
        //Connect to groq via API key
        //Give it the JSON formated message and wait for response
        //Return when data is avaialable
        //requres the image to be in base64 encoding
        public async Task<string> ImgGroq(string img)
        {
            //Get Authorization form groq
            string api_key = "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";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", api_key);

            string uri = "https://api.groq.com/openai/v1/chat/completions";

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(uri, requestImgJSON(img));
            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }


        //image json request format for groq to work
        JsonObject requestImgJSON(string base64_image)
        {
            string img = "data:image/jpeg;base64," + base64_image;

            var imgobj = new JsonObject
            {
                ["type"] = "image_url",
                ["image_url"] = new JsonObject
                {
                    ["url"] = img
                }
            };

            var contentmessage = new JsonObject
            {
                ["type"] = "text",
                ["text"] = "What is this picture in 25 words or less?"
            };

            var json = new JsonObject
            {
                ["model"] = "llama-3.2-11b-vision-preview",
                ["temperature"] = 1,
                ["max_tokens"] = 1024,
                ["top_p"] = 1,
                ["stream"] = false,
                ["messages"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["role"] = "user",
                        ["content"] = new JsonArray
                        {
                            imgobj, contentmessage
                        }
                    }
                }

            };

            return json;
        }
    }
}
