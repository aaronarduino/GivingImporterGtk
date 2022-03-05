using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcoAPI.Services
{
    public partial class GivingService
    {
        private readonly string ClientId;
        private readonly string ClientSecret;
        private string AuthenticationString { get { return $"{ClientId}:{ClientSecret}"; } }
        private Action<string> WriteToScreen;

        public readonly string ApiUrl;

        public GivingService(string apiUrl, string clientId, string clientSecret, Action<string> writeToScreen)
        {
            ApiUrl = apiUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
            WriteToScreen = writeToScreen;
        }

        private HttpClient _client;

        public HttpClient Client
        {
            get
            {
                if (_client == null)
                    _client = new HttpClient()
                    {
                        BaseAddress = new Uri(ApiUrl),
                    };
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(AuthenticationString));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                return _client;
            }
        }

        public async Task<bool> ExecutePost(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = await Client.PostAsync(request.RequestUri, request.Content);
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }

            if (httpResponseMessage.StatusCode == (HttpStatusCode)429)
                await Task.Delay(5000);
            return false;
        }

        public async Task<T> ExecutePost<T>(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = await Client.PostAsync(request.RequestUri, request.Content);
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(result);
                }
                catch (Exception ex)
                {
                    WriteToScreen("[ERROR]: " + ex.Message);
                }
            }
            if (httpResponseMessage.StatusCode == (HttpStatusCode)429)
                await Task.Delay(5000); // Hit rate limiter. Wait for delay time then try again
            return default;
        }

        public async Task<T> ExecuteGet<T>(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = await Client.SendAsync(request);
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(result);
                }
                catch (Exception ex)
                {
                    WriteToScreen("[ERROR]: " + ex.Message);
                }
            }
            return default;
        }
    }
}
