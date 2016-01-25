using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestEasy.Net
{
    public class RestClient
    {
        public async Task<T> GetAsync<T>(string requestUri, Dictionary<string, string> headers = null)
        {
            string response = await FetchLiveData(requestUri, headers);
            return JsonConvert.DeserializeObject<T>(response.Trim());
        }

        private static async Task<string> FetchLiveData(string requestUri, Dictionary<string, string> headers)
        {
            HttpClient httpClient = new HttpClient().AddHeaders(headers);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUri);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            return response;
        }

        public async Task<T> PostAsync<T>(string requestUri, Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null)
        {
            return await PostAsync<T>(requestUri, headers, parameters != null ? FormatPostParameters(parameters) : null);
        }

        public async Task<T> PostAsync<T>(string requestUri, Dictionary<string, string> headers = null,
            string content = null)
        {
            HttpClient client = new HttpClient().AddHeaders(headers);

            HttpContent httpContent = new StringContent(content ?? "");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage httpResponseMessage = await client.PostAsync(requestUri, httpContent);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected string FormatPostParameters(Dictionary<string, string> parameters)
        {
            string result = "";
            int count = 0;
            foreach (var parameter in parameters)
            {
                if (count > 0)
                {
                    result = result + "&";
                }
                result = result + parameter.Key + "=" + parameter.Value;
                count++;
            }
            return result;
        }
    }

    public static class HttpClientExtensions
    {
        public static HttpClient AddHeaders(this HttpClient httpClient, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            return httpClient;
        }
    }
}