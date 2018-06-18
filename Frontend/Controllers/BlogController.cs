using Frontend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Frontend.Models;
using Authentication_Service.Models;
using Newtonsoft.Json.Linq;

namespace Frontend.Controllers
{
    public class BlogController : Controller
    {
        private static WCFHelper _blogService = new WCFHelper();
        private static APIHelper _restApi = new APIHelper();
        // GET: Blog
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Strefa Dzika";
            var postsList = await _blogService.GetAllPosts();
            var commentsList = await _blogService.GetAllComments();

            var compoundView = new CompoundBlogView();
            compoundView.Posts = postsList;
            compoundView.Comments = commentsList;

            return View(compoundView);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPost(User user)
        {
            var jsonResponse = await _restApi.Login(user);
            var htmlMessage = "Nie udało się zalogować";
            if(jsonResponse["id"].ToObject<int>() != -1)
            {
                htmlMessage = "Zalogowałeś się jako " + user.username;
                Response.Cookies.Add(CreateLoginCookie(user, jsonResponse));
            }
            ViewBag.message = htmlMessage;
            return View();
        }

        private HttpCookie CreateLoginCookie(User user, JObject json)
        {
            var cookie = new HttpCookie("Login");
            var id = json["id"].ToObject<int>();
            var status = json["status"].ToObject<string>();
            cookie.Value = user.username + ";" + id + ";" + status;
            cookie.Expires = DateTime.Now.AddHours(1);
            return cookie;
        }
    }
}