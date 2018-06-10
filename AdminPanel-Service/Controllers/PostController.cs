using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using AdminPanel_Service.Models;
using NLog;

namespace AdminPanel_Service.Controllers
{
    /// <summary>
    /// Class providing action for administrator to delete and get posts.
    /// </summary>
    [RoutePrefix("post")]
    public class PostController : ApiController
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        [Route("")]
        [HttpGet]
        public JToken Get()
        {
            logger.Info("Get all posts request received.");

            String responseString;
            using (var client = new HttpClient())
            {
                responseString = client.GetStringAsync("http://localhost/Blog/GetAllPosts").ToString();
            }

            JToken response = JToken.Parse(responseString);
            logger.Info("Returning all posts succesfully performed!");
            return response;
        }

        [Route("")]
        [HttpPost]
        public JToken Get(JObject json)
        {
            logger.Info("Get all posts request received.");

            String responseString;
            using (var client = new HttpClient())
            {
                responseString = client.GetStringAsync("http://localhost/Blog/GetAllPosts").ToString();
            }

            JToken response = JToken.Parse(responseString);
            logger.Info("Returning all posts succesfully performed!");
            return response;
        }

        [Route("delete")]
        [HttpPost]
        public IHttpActionResult Delete(JObject json)
        {
            logger.Info("Delete post request received - attempting to delete post...");
            DeleteRequest request = json.ToObject<DeleteRequest>();
            


            logger.Info("Post has not been deleted due to insufficient permission.");
            return BadRequest("You don't have sufficient rights to delete posts!");
        }
    }
}
