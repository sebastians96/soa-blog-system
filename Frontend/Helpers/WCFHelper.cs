using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using BlogDataAccessLayer.Entity;
using BlogDataAccessLayer.Context;
using BlogsService;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using static BlogsService.BlogService;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Helpers
{
    public class WCFHelper
    {
        private static HttpClient _client = new HttpClient();

        public WCFHelper()
        {
            _client.BaseAddress = new Uri("http://localhost:8080/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var list = new List<Post>();
            var jsonObject = new JObject();
            HttpResponseMessage response = await _client.GetAsync("Blog/GetAllPosts");
            if(response.IsSuccessStatusCode)
            {
                jsonObject = await response.Content.ReadAsAsync<JObject>();
            }
            foreach(JObject singlePost in jsonObject["GetAllPostsResult"])
            {
                list.Add(singlePost.ToObject<Post>());
            }
            return list;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var list = new List<Comment>();
            var jsonObject = new JObject();
            HttpResponseMessage response = await _client.GetAsync("Blog/GetAllComments");
            if (response.IsSuccessStatusCode)
            {
                jsonObject = await response.Content.ReadAsAsync<JObject>();
            }
            foreach (JObject singleComment in jsonObject["GetAllCommentsResult"])
            {
                list.Add(singleComment.ToObject<Comment>());
            }
            return list;
        }

        public async Task<string> AddPost(PostWCF post)
        {
            var jsonObject = JsonConvert.SerializeObject(post);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("Blog/AddPost", content);
            if (response.IsSuccessStatusCode)
            {
                var wcfResponse = await response.Content.ReadAsAsync<string>();
                return wcfResponse;
            }
            return "Error";
        }

        public async Task<string> AddComment(CommentWCF comment)
        {
            var jsonObject = JsonConvert.SerializeObject(comment);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("Blog/AddComment", content);
            if (response.IsSuccessStatusCode)
            {
                var wcfResponse = await response.Content.ReadAsAsync<string>();
                return wcfResponse;
            }
            return "Error";
        }
    }
}