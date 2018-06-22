using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminPanel_Service.Controllers;
using AdminPanel_Service.Models;
using LiteDB;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using BlogsService;

namespace AdminPanel_Service.Tests
{
    [TestFixture]
    public class PostControllerTests
    {
        LiteDatabase database = new LiteDatabase(@"C:\test_database.db");
        [Test]
        public void Delete_ByAdmin()
        {
            var controller = new PostController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Delete(123);
            col.Insert(new User { username = "user", password = "user", id = 123, status = "admin" });
            var response = controller.Delete(JObject.Parse("{ \"id\":\"123\",\"delete\":\"125\"}"));

            Assert.AreNotEqual(null, response);
            var tmp = response as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Post has been deleted!", tmp.Content);
        }
        [Test]
        public void Delete_ByUser()
        {
            var controller = new PostController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Delete(123);
            col.Insert(new User { username = "user", password = "user", id = 123, status = "user" });
            IBlogService bs = new BlogService();
            bs.AddPost(new BlogService.PostWCF {User = "user", Date = "01.01.2001", Content = "some content 100g chicken", Title = "" });
            var response = controller.Delete(JObject.Parse("{ \"id\":\"123\",\"delete\":\"125\"}"));

            Assert.AreNotEqual(null, response);
            var tmp = response as BadRequestErrorMessageResult;
            Assert.AreEqual("You don't have sufficient rights to delete posts!", tmp.Message);
        }
    }
}