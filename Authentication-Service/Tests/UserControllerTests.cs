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
        LiteDatabase database = new LiteDatabase(@"C:\test_database.db");
        [Test]
        public void Register_UserDoesNotExistYet()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Delete(Query.EQ("username", "test"));
            var response = controller.Register(JObject.Parse("{\"username\": \"test\", \"password\": \"test\", \"status\": \"user\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreNotEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Register_UserDoesExist()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Insert(new User { username="test",password="test",status="user",id=0 });
            var response = controller.Register(JObject.Parse("{\"username\": \"test\", \"password\": \"test\", \"status\": \"user\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Register_UsernameEmpty()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Register(JObject.Parse("{\"username\": \"\", \"password\": \"test\", \"status\": \"user\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Register_PasswordEmpty()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Register(JObject.Parse("{\"username\": \"test\", \"password\": \"\", \"status\": \"user\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Register_StatusEmpty()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Register(JObject.Parse("{\"username\": \"test\", \"password\": \"\", \"status\": \"\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Login_UserExists()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Insert(new User { username = "test", password = "test", status = "user", id = 0 });
            var response = controller.Login(JObject.Parse("{\"username\": \"test\", \"password\": \"test\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreNotEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Login_UserDoesNotExist()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Delete(Query.EQ("username", "test"));
            var response = controller.Login(JObject.Parse("{\"username\": \"test\", \"password\": \"test\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Exist_UserDoesNotExist()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Delete(Query.EQ("username", "test"));
            var response = controller.Login(JObject.Parse("{\"username\": \"test\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
        [Test]
        public void Exist_UserDoesExist()
        {
            var controller = new UserController(database);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            LiteDatabase db = database;
            var col = db.GetCollection<User>();
            col.Insert(new User { username = "test", password = "test", status = "user", id = 0 });
            var response = controller.Login(JObject.Parse("{\"username\": \"test\"}"));

            // Assert
            Assert.AreNotEqual(null, response["id"]);
            Assert.AreEqual(-1, (int)response["id"]);
        }
    }
}