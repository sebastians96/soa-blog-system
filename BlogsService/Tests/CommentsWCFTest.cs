using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsService.Tests
{
    [TestFixture]
    public class CommentsWCFTest
    {
        private BlogService _blogService;

        public CommentsWCFTest()
        {
            this._blogService = new BlogService();
        }

        [Test]
        public void GetAllCommentsTest()
        {
            var response = _blogService.GetAllComments();
            Assert.NotNull(response);
        }

        [Test]
        public void GetCommentByIDTest()
        {
            var response = _blogService.GetCommentById(2);

        }

        [Test]
        public void PostCommentTest()
        {

        }

        [Test]
        public void DeleteCommentTest()
        {

        }
    }
}
