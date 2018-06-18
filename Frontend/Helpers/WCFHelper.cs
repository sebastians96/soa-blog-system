﻿using System;
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
            var ser = new DataContractJsonSerializer(typeof(List<Post>));
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
            var ser = new DataContractJsonSerializer(typeof(List<Post>));
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
    }
}