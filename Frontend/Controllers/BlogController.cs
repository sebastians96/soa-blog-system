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
using BlogDataAccessLayer.Entity;
using static BlogsService.BlogService;
using BlogsService;

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

        public ActionResult Add()
        {
            return View();
        }

        public async Task<ActionResult> Admin()
        {
            var cookie = Request.Cookies["Login"];
            if (cookie != null && cookie.Value.Contains("admin"))
            {
                var postsList = await _blogService.GetAllPosts();
                return View(postsList);
            }
            else
            {
                return View("PermissionError");
            }
        }

        public ActionResult AddComment(int id)
        {
            var cookie = new HttpCookie("postID");
            cookie["id"] = id.ToString();
            cookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(cookie);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPost(User user)
        {
            var jsonResponse = await _restApi.Login(user);
            var htmlMessage = "Nie udało się zalogować";
            if (jsonResponse["id"].ToObject<int>() != -1)
            {
                htmlMessage = "Zalogowałeś się jako " + user.username;
                Response.Cookies.Add(CreateLoginCookie(user, jsonResponse));
            }
            ViewBag.message = htmlMessage;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPost(User user)
        {
            var jsonResponse = await _restApi.Register(user);
            var htmlMessage = "Nie udało się zarejestrować";
            if (jsonResponse["id"].ToObject<int>() != -1)
            {
                htmlMessage = "Zarejestrowałeś się jako " + user.username;
                Response.Cookies.Add(CreateLoginCookie(user, jsonResponse));
            }
            ViewBag.message = htmlMessage;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPost(Post post)
        {
            var cookie = Request.Cookies["Login"];
            if(cookie != null && cookie["status"].Contains("admin"))
            {
                post.User = cookie["username"];
                var status = await _blogService.AddPost(postWCFChanger(post));
                if(status.Equals("Error"))
                {
                    return View("Error");
                }
                return View();
            } else
            {
                return View("PermissionError");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> AddCommentPost(Comment comment)
        {
            var cookie = Request.Cookies["Login"];
            var idCookie = Request.Cookies["postID"];
            if (cookie != null)
            {
                if (idCookie != null)
                {
                    comment.User = cookie["username"];
                    comment.PostID = int.Parse(idCookie["id"]);

                    var c = new HttpCookie("postID");
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);

                    var status = await _blogService.AddComment(commentWCFChanger(comment));
                    if (status.Equals("Error"))
                    {
                        return View("Error");
                    }
                    return View();
                } else
                {
                    return View("Error");
                }
            }
            else
            {
                return View("PermissionError");
            }
        }

        public ActionResult Logout()
        {
            if(Request.Cookies["Login"] != null)
            {
                var c = new HttpCookie("Login");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                return View("LogoutPost");
            } else
            {
                return View("Error");
            }
            
        }

        private HttpCookie CreateLoginCookie(User user, JObject json)
        {
            var cookie = new HttpCookie("Login");
            var id = json["id"].ToObject<int>();
            var status = json["status"].ToObject<string>();
            cookie["username"] = user.username;
            cookie["id"] = id.ToString();
            cookie["status"] = status;
            cookie.Expires = DateTime.Now.AddHours(1);
            return cookie;
        }

        private PostWCF postWCFChanger(Post post)
        {
            var t = new PostWCF();
            t.Content = post.Content;
            t.Date = post.Date;
            t.Title = post.Title;
            t.User = post.User;
            return t;
        }

        private CommentWCF commentWCFChanger(Comment comment)
        {
            var t = new CommentWCF();
            t.Content = comment.Content;
            t.Date = comment.Date;
            t.User = comment.User;
            t.PostID = comment.PostID;
            return t;
        }
    }
}