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
    public class UserController : ApiController
    {
        LiteDatabase db = new LiteDatabase(@"C:\database.db");
        [Route("register")]
        [HttpPost]
        public JObject Register(JObject json)
        {
            JObject tmp = new JObject();
            User user = json.ToObject<User>();
            var col = db.GetCollection<User>();
            if (String.IsNullOrEmpty(user.username) || String.IsNullOrEmpty(user.password) || String.IsNullOrEmpty(user.status) || col.Exists(Query.EQ("username",user.username)))
            {
                tmp.Add("id", -1);
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
                    return tmp;
                }
                else
                {
                    
                    tmp.Add("id", -1);
                    return tmp;
                }

            }
        }
        [Route("login")]
        [HttpPost]
        public JObject Login(JObject json)
        {
            Request user = json.ToObject<Request>();
            User current = db.GetCollection<User>().Find(Query.EQ("username", user.username)).Where(x => x.password == user.password).FirstOrDefault();
            JObject tmp = new JObject();
            if (current != null)
            {
                tmp.Add("id", current.id);
                tmp.Add("status", current.status);
                return tmp;
            }
            else
            {
                tmp.Add("id", -1);
                return tmp;
            }
        }

        
    }

}
