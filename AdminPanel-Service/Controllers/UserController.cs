using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminPanel_Service.Models;
using LiteDB;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AdminPanel_Service.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        LiteDatabase db = new LiteDatabase(@"C:\database.db");
        [Route("")]
        [HttpGet]
        public JArray Get()
        {
            var col = db.GetCollection<User>();
            JArray response = new JArray();
            List<User> list = col.FindAll().ToList<User>();
            foreach(User u in list)
            {
                response.Add((JObject)JsonConvert.DeserializeObject("{ \"id\": " + u.id + ", \"username\": \"" + u.username + "\", \"status\": \"" + u.status + "\" }"));
            }
            //var tmp = new { users = list };
            return response;//JObject.Parse(JsonConvert.SerializeObject(tmp));//JsonConvert.SerializeObject(list, Formatting.Indented);
        }

        [Route("delete")]
        [HttpPost]
        public IHttpActionResult Delete(JObject json)
        {
            DeleteRequest request = json.ToObject<DeleteRequest>();
            var col = db.GetCollection<User>();
            if (col.FindById(request.id).status == "admin")
            {
                if (col.FindById(request.delete)!=null)
                {
                    col.Delete(request.delete);
                    return Ok("User has been deleted!");
                }
                else
                {
                    return BadRequest("There is no user with this id in the database!");
                }
            }
            return BadRequest("You don't have sufficient rights to delete the user!");
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update(JObject json)
        {
            UpdateRequest request = json.ToObject<UpdateRequest>();
            var col = db.GetCollection<User>();
            if (col.FindById(request.id).status == "admin")
            {
                if (col.FindById(request.update) != null)
                {
                    User tmp = col.FindById(request.update);
                    tmp.status = request.status;
                    col.Update(request.update, tmp);
                    return Ok("User status has been updated!");
                }
                else
                {
                    return BadRequest("There is no user with this id in the database!");
                }
            }
            return BadRequest("You don't have sufficient rights to change status of the user!");
        }
    }
}
