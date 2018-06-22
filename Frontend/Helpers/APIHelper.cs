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
        private static HttpClient _adminClient = new HttpClient();


        public APIHelper()
        {
            _client.BaseAddress = new Uri("http://localhost:55717/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _adminClient.BaseAddress = new Uri("http://localhost:51325/");
            _adminClient.DefaultRequestHeaders.Accept.Clear();
            _adminClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

        public async Task<HttpResponseMessage> DeletePost(int id)
        {
            HttpResponseMessage response = await _adminClient.PostAsJsonAsync<int>("/post/delete", id);
            return response;
        }

        public async Task<JObject> Update(User user)
        {
            HttpResponseMessage response = await _adminClient.PostAsJsonAsync<User>("/admin/user/update", user);
            var jsonObject = await response.Content.ReadAsAsync<JObject>();
            return jsonObject;
        }

    }
}