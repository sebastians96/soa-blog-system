using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LiteDB;
using NLog;
using Authentication_Service.Models;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Authentication_Service.Controllers
{
    /// <summary>
    /// User Controller allowing users to register and log in.
    /// </summary>
    public class UserController : ApiController
    {
        LiteDatabase db;
        public UserController()
        {
            db = new LiteDatabase(@"C:\database.db");
        }
        public UserController(LiteDatabase database)
        {
            db = database;
        }
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// This function is registering new users.
        /// </summary>
        /// <remarks>It reads the request from json object, checks if the user does not already exist in LiteDB and creates new entry.</remarks>
        /// <param name="json"></param>
        /// <returns>Json containing id and status of the user if the user has been succesfully registered or json with id -1 if user already exist or something went wrong.</returns>
        [Route("register")]
        [HttpPost]
        public JObject Register(JObject json)
        {
            logger.Info("Register post request received - attempting to register new user.");
            JObject tmp = new JObject();
            User user = json.ToObject<User>();
            var col = db.GetCollection<User>();
            if (String.IsNullOrEmpty(user.username) || String.IsNullOrEmpty(user.password) || String.IsNullOrEmpty(user.status) || col.Exists(Query.EQ("username",user.username)))
            {
                tmp.Add("id", -1);
                logger.Info("Bad request or user already exist.");
                return tmp;
            }
           else
            {
                user.id = 0;
                col.Insert(user);
                var current = col.FindOne(Query.EQ("username", user.username));
                if (current != null)
                {
                    tmp.Add("id", current.id);
                    tmp.Add("status", current.status);
                    logger.Info("User has been registered succesfully.");
                    return tmp;
                }
                else
                {
                    logger.Info("There was a problem with the database insertion.");
                    tmp.Add("id", -1);
                    return tmp;
                }

            }
        }
        /// <summary>
        /// This function allows to check if user exist when logging in.
        /// </summary>
        /// <remarks>It reads the requst from the json object checks if user exist in LiteDB and if the password is correct and then returns user id and status.</remarks>
        /// <param name="json"></param>
        /// <returns>Json with user id and status if logging in has been succesfull or otherwise json with id -1.</returns>
        [Route("login")]
        [HttpPost]
        public JObject Login(JObject json)
        {
            logger.Info("Login post request received - searching for user in the database");
            Request user = json.ToObject<Request>();
            User current = db.GetCollection<User>().Find(Query.EQ("username", user.username)).Where(x => x.password == user.password).FirstOrDefault();
            JObject tmp = new JObject();
            if (current != null)
            {
                logger.Info("User has been found - returning id and status");
                tmp.Add("id", current.id);
                tmp.Add("status", current.status);
                return tmp;
            }
            else
            {
                logger.Info("User does not exist in the database.");
                tmp.Add("id", -1);
                return tmp;
            }
        }

        [Route("user/exist")]
        [HttpPost]
        public JObject Exist(JObject json)
        {
            logger.Info("Exist post request received - searching for user in the database");
            ExistRequest user = json.ToObject<ExistRequest>();
            User current = db.GetCollection<User>().Find(Query.EQ("username", user.username)).FirstOrDefault();
            JObject tmp = new JObject();
            if (current != null)
            {
                logger.Info("User has been found in the database.");
                tmp.Add("exist", true);
                return tmp;
            }
            else
            {
                logger.Info("User does not exist in the database.");
                tmp.Add("exist", false);
                return tmp;
            }
        }

    }

}
