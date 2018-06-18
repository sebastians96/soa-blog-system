using Frontend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Frontend.Models;
using Authentication_Service.Models;

namespace Frontend.Controllers
{
    public class BlogController : Controller
    {
        private static WCFHelper _blogService = new WCFHelper();
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

        [HttpPost]
        public ActionResult LoginPost(User user)
        {
            var t = new User();
            return View();
        }
    }
}