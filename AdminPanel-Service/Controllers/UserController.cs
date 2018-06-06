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
    /// <summary>
    /// Class providing actions for the administrator to update and delete users.
    /// </summary>
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        LiteDatabase db = new LiteDatabase(@"C:\database.db");
        [Route("")]
        [HttpGet]
        public JArray Get()
        {
            logger.Info("Get request received - returning the list of all users in the database...");
            var col = db.GetCollection<User>();
            JArray response = new JArray();
            List<User> list = col.FindAll().ToList<User>();
            foreach(User u in list)
            {
                response.Add((JObject)JsonConvert.DeserializeObject("{ \"id\": " + u.id + ", \"username\": \"" + u.username + "\", \"status\": \"" + u.status + "\" }"));
            }
            logger.Info("Returning all users succesfully performed!");
            return response;
        }

        [Route("delete")]
        [HttpPost]
        public IHttpActionResult Delete(JObject json)
        {
            logger.Info("Delete post request received - attempting to delete the user...");
            DeleteRequest request = json.ToObject<DeleteRequest>();
            var col = db.GetCollection<User>();
            if (col.FindById(request.id).status == "admin")
            {
                if (col.FindById(request.delete)!=null)
                {
                    col.Delete(request.delete);
                    logger.Info("User has been succesfully deleted.");
                    return Ok("User has been deleted!");
                }
                else
                {
                    logger.Info("User not found in the database.");
                    return BadRequest("There is no user with this id in the database!");
                }
            }
            logger.Info("User has not been deleted from the database due to insufficient permission.");
            return BadRequest("You don't have sufficient rights to delete the user!");
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update(JObject json)
        {
            logger.Info("Update post request received - attempting to update user...");
            UpdateRequest request = json.ToObject<UpdateRequest>();
            var col = db.GetCollection<User>();
            if (col.FindById(request.id).status == "admin")
            {
                if (col.FindById(request.update) != null)
                {
                    User tmp = col.FindById(request.update);
                    tmp.status = request.status;
                    col.Update(request.update, tmp);
                    logger.Info("User status succesfully updated.");
                    return Ok("User status has been updated!");
                }
                else
                {
                    logger.Info("User not found in the database.");
                    return BadRequest("There is no user with this id in the database!");
                }
            }
            logger.Info("User has not been updated in the database due to insufficient permission.");
            return BadRequest("You don't have sufficient rights to change status of the user!");
        }
    }
}
