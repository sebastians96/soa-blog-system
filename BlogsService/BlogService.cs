﻿using BlogDataAccessLayer.Context;
using BlogDataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Authentication_Service.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlogsService
{
    public class BlogService : IBlogService
    {
        private readonly IBlogContext _blogContext;
        private readonly UserController _userController;

        public BlogService()
        {
            this._blogContext = new BlogContext();
            this._userController = new UserController();
        }

        //Comments

        public string AddComment(CommentWCF comment)
        {
            var response = _userController.Exist(JObject.Parse("{\"username\": \"" + comment.User + "\"}"));
            if ((Boolean) response["exist"])
            {
                var post = _blogContext.Posts.ToList().Find(p => p.PostID == comment.PostID);
                if (post == null) return "Error there is no such PostId";
                else
                {
                    var commentDB = new Comment() { User = comment.User, Content = comment.Content, Date = comment.Date, PostID = comment.PostID };
                    _blogContext.Comments.Add(commentDB);
                    _blogContext.SaveChanges();
                    return "Comment added";
                }
            }
            else
            {
                return "There is no such user";
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
            public string Title { get; set; }
        }
    }
}
