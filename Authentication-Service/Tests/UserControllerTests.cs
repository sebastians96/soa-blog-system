using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authentication_Service.Controllers;
using Authentication_Service.Models;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using LiteDB;

namespace Authentication_Service.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void Register_UserDoesNotExistYet()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if(col.Exists(Query.EQ("username","test"))) col.Delete(Query.EQ("username", "test"));
            var response = controller.Register(JObject.Parse("{\"username\": \"test\", \"password\": \"test\", \"status\": \"user\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreNotEqual(-1, response["id"]);
        }
    }
}