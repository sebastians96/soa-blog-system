using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using AdminPanel_Service.Models;
using NLog;
using BlogsService;
using LiteDB;

namespace AdminPanel_Service.Controllers
{
    /// <summary>
    /// Class providing action for administrator to delete and get posts.
    /// </summary>
    [RoutePrefix("post")]
    public class PostController : ApiController
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IBlogService bs = new BlogService();
        LiteDatabase db;
        public PostController()
        {
            db = new LiteDatabase(@"C:\database.db");
        }
        public PostController(LiteDatabase database)
        {
            db = database;
        }

        [Route("delete")]
        [HttpPost]
        public IHttpActionResult Delete(JObject json)
        {
            logger.Info("Delete post request received - attempting to delete post...");
            DeleteRequest request = json.ToObject<DeleteRequest>();

            var col = db.GetCollection<User>();
            if (col.FindById(request.id).status == "admin")
            {
                bs.DeletePost(request.delete);
                logger.Info("Post has been deleted!");
                return Ok("Post has been deleted!");
            }

            logger.Info("Post has not been deleted due to insufficient permission.");
            return BadRequest("You don't have sufficient rights to delete posts!");
        }
    }
}
