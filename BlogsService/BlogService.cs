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
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public BlogService()
        {
            this._blogContext = new BlogContext();
        }

        //Comments

        public string AddComment(CommentWCF comment)
        {
            logger.Info("Registeres POST request - attempting to add new comment.");
            var post = _blogContext.Posts.ToList().Find(p => p.PostID == comment.PostID);
            if (post == null)
            {
                logger.Warn("Cannot add comment - There is no such PostID.");
                return "Error there is no such PostId.";
            }
            else
            {
                var commentDB = new Comment() { User = comment.User, Content = comment.Content, Date = comment.Date, PostID = comment.PostID };
                _blogContext.Comments.Add(commentDB);
                _blogContext.SaveChanges();

                logger.Info("New comment added.");
                return "Comment added.";
            }
        }

        public List<Comment> GetAllComments()
        {
            logger.Info("Registered GET request - returning all comments.");
            return _blogContext.Comments.ToList();
        }

        public Comment GetCommentById(int id)
        {
            logger.Info("Registered GET request - returning comment with id:{1}",id);
            return _blogContext.Comments.ToList().Find(c => c.CommentID == id);
        }

        public Comment DeleteComment(int id)
        {
            logger.Info("Registered DELETE request - attempting to delete comment with id:{1}", id);
            var itemToDelete = _blogContext.Comments.ToList().Find(c => c.CommentID == id);
            if (itemToDelete == null)
            {
                logger.Warn("Cannot delete comment with id:{1} - there is no such comment", id);
                return null;
            }
            _blogContext.Comments.Remove(itemToDelete);
            _blogContext.SaveChanges();
            logger.Info("Comment deleted");
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

        // Posts

        public string AddPost(PostWCF post)
        {
            var postDB = new Post() { User = post.User, Content = post.Content, Date = post.Date };
            _blogContext.Posts.Add(postDB);
            _blogContext.SaveChanges();
            return "Post added";
        }

        public List<Post> GetAllPosts()
        {
            return _blogContext.Posts.ToList();
        }

        public Post GetPostById(int id)
        {
            return _blogContext.Posts.ToList().Find(c => c.PostID == id);
        }

        public Post DeletePost(int id)
        {
            var itemToDelete = _blogContext.Posts.ToList().Find(c => c.PostID == id);
            if (itemToDelete == null) return null;
            _blogContext.Posts.Remove(itemToDelete);
            _blogContext.SaveChanges();
            return itemToDelete;
        }

        [DataContract]
        public class PostWCF
        {
            [DataMember]
            public string User { get; set; }
            [DataMember]
            public string Date { get; set; }
            [DataMember]
            public string Content { get; set; }
            [DataMember]
            public int Title { get; set; }
        }
    }
}
