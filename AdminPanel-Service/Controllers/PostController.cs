using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using AdminPanel_Service.Models;

namespace AdminPanel_Service.Controllers
{
    [RoutePrefix("post")]
    public class PostController : ApiController
    {
        [Route("delete")]
        [HttpPost]
        public IHttpActionResult Delete(JObject json)
        {
            DeleteRequest request = json.ToObject<DeleteRequest>();
           
            // Deleting post action

            return Ok();
        }
    }
}
