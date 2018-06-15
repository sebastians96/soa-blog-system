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
    public class CommentsWCFTest
    {
        private readonly BlogService _blogService;
        private readonly IBlogContext _blogContext;
        private readonly UserController _userController;

        public CommentsWCFTest()
        {
            this._blogService = new BlogService();
            this._blogContext = new BlogContext();
            this._userController = new UserController();
        }

        [Test]
        public void GetAllCommentsTest()
        {
            var count1 = _blogService.GetAllComments().Count;
            var count2 = _blogContext.Comments.Count();
            Assert.AreEqual(count1, count2);
        }

        [Test]
        public void GetCommentByIDTest()
        {
            var response1 = _blogService.GetCommentById(0);
            Assert.Null(response1);
            var response2 = _blogService.GetCommentById(1);
            Assert.NotNull(response2);
        }

        [Test]
        public void PostCommentTest()
        {
            var responseUser = _userController.Register(JObject.Parse("{\"username\": \"Test\", \"password\": \"test\", \"status\": \"user\"}"));
            Assert.AreNotEqual(null, responseUser["id"]);

            var count1 = _blogContext.Comments.Count();
            _blogService.AddComment(new BlogService.CommentWCF() { User = "Test", Content = "Test", Date = "01.02.2015", PostID = 1 });
            var count2 = _blogContext.Comments.Count();
            Assert.AreEqual(count1 + 1, count2);
            var itemToDelete = _blogContext.Comments.ToList().FindAll(c => c.Content.Equals("Test"));
            _blogContext.Comments.RemoveRange(itemToDelete);
        }

        [Test]
        public void DeleteCommentTest()
        {
            _blogService.AddComment(new BlogService.CommentWCF() { User = "Test", Content = "Test", Date = "01.01.0000", PostID = 1 });
            var itemToDelete = _blogContext.Comments.ToList().Find(c => c.Content.Equals("Test"));
            var count1 = _blogContext.Comments.Count();
            _blogService.DeleteComment(itemToDelete.CommentID);
            var count2 = _blogContext.Comments.Count();
            Assert.AreEqual(count1, count2 + 1);

        }
    }
}
