using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.utils
{
    public static class KeycloakUtility
    {
        public static async Task<HttpResponseMessage> CallApiMethod(HttpMethod method, string url, List<KeyValuePair<string, string>> options, string accesstoken = null)
        {
            var client = new HttpClient();
            var req = new HttpRequestMessage(method, url) { Content = new FormUrlEncodedContent(options) };
            if (accesstoken != null)
                req.Headers.Add("Authorization", accesstoken);
            return await client.SendAsync(req);
        }

        public static async Task<HttpResponseMessage> CallJsonMethod(HttpMethod method, string url, string accesstoken, string body = null)
        {
            var client = new HttpClient();
            HttpRequestMessage req;
            if (string.IsNullOrEmpty(body))
                req = new HttpRequestMessage(method, url);
            else
                req = new HttpRequestMessage(method, url) { 
                    Content = new StringContent(body, Encoding.UTF8, "application/json") 
                };
            req.Headers.Add("Authorization", accesstoken);
            return await client.SendAsync(req);
        }
    }
}
