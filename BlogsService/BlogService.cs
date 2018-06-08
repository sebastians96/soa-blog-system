using BlogDataAccessLayer.Context;
using BlogDataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BlogsService
{
    public class BlogService : IBlogService
    {
        private readonly IBlogContext _blogContext;

        public BlogService()
        {
            this._blogContext = new BlogContext();
        }

        //Comments

        public string AddComment(CommentWCF comment)
        {
            var post = _blogContext.Posts.ToList().Find(p => p.PostID == comment.PostID);
            if (post == null) return "Error there is no such PostId";
            else
            {
                var commentDB = new Comment() { User = comment.User, Date = comment.Date, PostID = comment.PostID };
                _blogContext.Comments.Add(commentDB);
                _blogContext.SaveChanges();
                return "Comment added";
            }
        }

        public List<Comment> GetAllComments()
        {
            return _blogContext.Comments.ToList();
        }

        public Comment GetCommentById(int id)
        {
            return _blogContext.Comments.ToList().Find(c => c.CommentID == id);
        }

        public Comment DeleteComment(int id)
        {
            var itemToDelete = _blogContext.Comments.ToList().Find(c => c.CommentID == id);
            if (itemToDelete == null) return null;
            _blogContext.Comments.Remove(itemToDelete);
            _blogContext.SaveChanges();
            return itemToDelete;
        }

        [DataContract]
        public class CommentWCF
        {
            [DataMember]
            public string User { get; set; }
            [DataMember]
            public string Date { get; set; }
            [DataMember]
            public string Content { get; set; }
            [DataMember]
            public int PostID { get; set; }
        }



        //// Posts


    }
}
