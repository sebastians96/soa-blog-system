using Authentication_Service.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Frontend.Helpers
{
    public class APIHelper
    {
        private static HttpClient _client = new HttpClient();

        public APIHelper()
        {
            _client.BaseAddress = new Uri("http://localhost:55717/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<JObject> Login(User user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<User>("/login", user);
            var jsonObject = await response.Content.ReadAsAsync<JObject>();
            return jsonObject;
        }

        public async Task<JObject> Register(User user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<User>("/register", user);
            var jsonObject = await response.Content.ReadAsAsync<JObject>();
            return jsonObject;
        }
    }
}