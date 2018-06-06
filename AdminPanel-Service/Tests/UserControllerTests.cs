using AdminPanel_Service.Controllers;
using AdminPanel_Service.Models;
using LiteDB;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace AdminPanel_Service.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void Get()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            col.Insert(new User { username = "user", password = "user", id = 0, status = "user" });
            var response = controller.Get();

            // Assert
            Assert.AreNotEqual(null, response);
        }
        [Test]
        public void Delete_UserDoesExist()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if (!col.Exists(Query.EQ("Id", 9998))) col.Delete(9998);
            col.Insert(new User { username = "test", password = "test", status = "user", id = 9998 });
            if (!col.Exists(Query.EQ("Id", 9999))) col.Delete(9999);
            col.Insert(new User { username = "admin", password = "admin", status = "admin", id = 9999 });
            var response = controller.Delete(JObject.Parse("{\"id\": 9999, \"delete\": 9998 }"));
            // Assert
            Assert.AreNotEqual(null, response);
            var tmp = response as OkNegotiatedContentResult<string>;
            Assert.AreEqual("User has been deleted!", tmp.Content);

        }
        [Test]
        public void Delete_UserDoesNotExist()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if (!col.Exists(Query.EQ("Id", 9998))) col.Delete(9998);
            if (!col.Exists(Query.EQ("Id", 9999))) col.Delete(9999);
            col.Insert(new User { username = "admin", password = "admin", status = "admin", id = 9999 });
            var response = controller.Delete(JObject.Parse("{\"id\": 9999, \"delete\": 9998 }"));
            // Assert
            Assert.AreNotEqual(null, response);
            var tmp = response as BadRequestErrorMessageResult;
            Assert.AreEqual("There is no user with this id in the database!", tmp.Message);

        }
        [Test]
        public void Delete_InsufficientPermission()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if (!col.Exists(Query.EQ("Id", 9998))) col.Delete(9998);
            col.Insert(new User { username = "test", password = "test", status = "user", id = 9998 });
            if (!col.Exists(Query.EQ("Id", 9999))) col.Delete(9999);
            col.Insert(new User { username = "admin", password = "admin", status = "user", id = 9999 });
            var response = controller.Delete(JObject.Parse("{\"id\": 9999, \"delete\": 9998 }"));
            // Assert
            Assert.AreNotEqual(null, response);
            var tmp = response as BadRequestErrorMessageResult;
            Assert.AreEqual("You don't have sufficient rights to delete the user!", tmp.Message);

        }
        [Test]
        public void Update_UserDoesExist()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if (!col.Exists(Query.EQ("Id", 9998))) col.Delete(9998);
            col.Insert(new User { username = "test", password = "test", status = "user", id = 9998 });
            if (!col.Exists(Query.EQ("Id", 9999))) col.Delete(9999);
            col.Insert(new User { username = "admin", password = "admin", status = "admin", id = 9999 });
            var response = controller.Update(JObject.Parse("{\"id\": 9999, \"update\": 9998, \"status\": \"admin\" }"));
            // Assert
            Assert.AreNotEqual(null, response);
            var tmp = response as OkNegotiatedContentResult<string>;
            Assert.AreEqual("User status has been updated!", tmp.Content);

        }
        [Test]
        public void Update_UserDoesNotExist()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if (!col.Exists(Query.EQ("Id", 9998))) col.Delete(9998);
            if (!col.Exists(Query.EQ("Id", 9999))) col.Delete(9999);
            col.Insert(new User { username = "admin", password = "admin", status = "admin", id = 9999 });
            var response = controller.Update(JObject.Parse("{\"id\": 9999, \"update\": 9998, \"status\": \"admin\" }"));
            // Assert
            Assert.AreNotEqual(null, response);
            var tmp = response as BadRequestErrorMessageResult;
            Assert.AreEqual("There is no user with this id in the database!", tmp.Message);

        }
        [Test]
        public void Update_InsufficientPermission()
        {
            var controller = new UserController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = new LiteDatabase(@"C:\database.db");
            var col = db.GetCollection<User>();
            if (!col.Exists(Query.EQ("Id", 9998))) col.Delete(9998);
            col.Insert(new User { username = "test", password = "test", status = "user", id = 9998 });
            if (!col.Exists(Query.EQ("Id", 9999))) col.Delete(9999);
            col.Insert(new User { username = "admin", password = "admin", status = "user", id = 9999 });
            var response = controller.Update(JObject.Parse("{\"id\": 9999, \"update\": 9998, \"status\": \"admin\" }"));
            // Assert
            Assert.AreNotEqual(null, response);
            var tmp = response as BadRequestErrorMessageResult;
            Assert.AreEqual("You don't have sufficient rights to change status of the user!", tmp.Message);

        }
    }
}