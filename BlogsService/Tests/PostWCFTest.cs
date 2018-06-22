using Authentication_Service.Controllers;
using BlogDataAccessLayer.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication_Service.Models;
using Newtonsoft.Json.Linq;

namespace BlogsService.Tests
{
    [TestFixture]
    public class PostsWCFTest
    {
        private readonly BlogService _blogService;
        private readonly IBlogContext _blogContext;
        private readonly UserController _userController;

        public PostsWCFTest()
        {
            this._blogService = new BlogService();
            this._blogContext = new BlogContext();
            this._userController = new UserController();
        }

        [Test]
        public void GetAllPostsTest()
        {
            var count1 = _blogService.GetAllPosts().Count;
            var count2 = _blogContext.Posts.Count();
            Assert.AreEqual(count1, count2);
        }

        [Test]
        public void GetPostByIDTest()
        {
            var response1 = _blogService.GetPostById(0);
            Assert.Null(response1);
            var response2 = _blogService.GetPostById(1);
            Assert.NotNull(response2);
        }

        [Test]
        public void PostPostTest()
        {
            var responseUser = _userController.Register(JObject.Parse("{\"username\": \"Test\", \"password\": \"test\", \"status\": \"user\"}"));
            Assert.AreNotEqual(null, responseUser["id"]);

            var count1 = _blogContext.Posts.Count();
            _blogService.AddPost(new BlogService.PostWCF() { User = "Test", Content = "for breakfast i ate 2 eggs", Date = "01.02.2015", Title = "Tytul" });
            var count2 = _blogContext.Posts.Count();
            Assert.AreEqual(count1 + 1, count2);
            var itemToDelete = _blogContext.Posts.ToList().FindAll(c => c.User.Equals("Test"));
            _blogContext.Posts.RemoveRange(itemToDelete);
        }

        [Test]
        public void DeletePostTest()
        {
            _blogService.AddPost(new BlogService.PostWCF()
            {
                User = "Test",
                Content = "for breakfast i ate 2 eggs",
                Date = "01.01.0000",
                Title = "tytul" });
            var itemToDelete = _blogContext.Posts.ToList().Find(c => c.User.Equals("Test"));
            var count1 = _blogContext.Posts.Count();
            _blogService.DeletePost(itemToDelete.PostID);
            var count2 = _blogContext.Posts.Count();
            Assert.AreEqual(count1, count2 + 1);

        }
    }
}
