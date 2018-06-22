using BlogDataAccessLayer.Context;
using BlogDataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Authentication_Service.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NutritionAPI.Controllers;
using System.Net;
using System.IO;

namespace BlogsService
{
    public class BlogService : IBlogService
    {
        private readonly IBlogContext _blogContext;
        private readonly UserController _userController;
        private readonly NutritionController _nutritionController;
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public BlogService()
        {
            this._blogContext = new BlogContext();
            this._userController = new UserController();
            this._nutritionController = new NutritionController();
        }

        //Comments

        public string AddComment(CommentWCF comment)
        {
            logger.Info("Registeres POST request - attempting to add new comment.");
            var response = _userController.Exist(JObject.Parse("{\"username\": \"" + comment.User + "\"}"));
            if ((Boolean) response["exist"])
            {
                var post = _blogContext.Posts.ToList().Find(p => p.PostID == comment.PostID);
                if (post == null)
                {
                    logger.Warn("Cannot add comment - There is no such PostID.");
                    return "Error there is no such PostId";
                }
                else
                {

                    var calories = getCalories(comment.Content);
                    var content1 = comment.Content + " Counted calories:" + calories;
                    var commentDB = new Comment() { User = comment.User, Content = content1, Date = comment.Date, PostID = comment.PostID };
                    _blogContext.Comments.Add(commentDB);
                    _blogContext.SaveChanges();
                    logger.Info("New comment added.");
                    return "Comment added";
                }
            }
            else
            {
                logger.Info("Cannot add Comment - there is no such user.");
                return "There is no such user";
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

        [DataContract(Name = "Response")]
        public class CommentWCF
        {
            [DataMember(Name = "User")]
            public string User { get; set; }
            [DataMember(Name = "Date")]
            public string Date { get; set; }
            [DataMember(Name = "Content")]
            public string Content { get; set; }
            [DataMember(Name = "PostID")]
            public int PostID { get; set; }
        }

        // Posts

        public string AddPost(PostWCF post)
        {
            logger.Info("Registeres POST request - attempting to add new post.");
            var response = _userController.Exist(JObject.Parse("{\"username\": \"" + post.User + "\"}"));
            if ((Boolean)response["exist"])
            {
                var calories = getCalories(post.Content);
                var content1 = post.Content + " Counted calories:" + calories;
                var postDB = new Post() { User = post.User, Content = content1, Date = post.Date, Title = post.Title };
                _blogContext.Posts.Add(postDB);
                _blogContext.SaveChanges();
                logger.Info("New post added.");
                return "Post added";

            }
            else
            {
                logger.Info("Cannot add Post - there is no such user.");
                return "There is no such user";
            }
        }

        public List<Post> GetAllPosts()
        {
            logger.Info("Registered GET request - returning all posts.");
            return _blogContext.Posts.ToList();
        }

        public Post GetPostById(int id)
        {
            logger.Info("Registered GET request - returning post with id:{1}", id);
            return _blogContext.Posts.ToList().Find(c => c.PostID == id);
        }

        public Post DeletePost(int id)
        {
            logger.Info("Registered DELETE request - attempting to delete post with id:{1}", id);
            var itemToDelete = _blogContext.Posts.ToList().Find(c => c.PostID == id);
            if (itemToDelete == null)
            {
                logger.Warn("Cannot delete post with id:{1} - there is no such post", id);
                return null;
            }
            _blogContext.Posts.Remove(itemToDelete);
            _blogContext.SaveChanges();
            logger.Info("Post deleted");
            return itemToDelete;
        }

        [DataContract(Name = "Response")]
        public class PostWCF
        {
            [DataMember(Name = "User")]
            public string User { get; set; }
            [DataMember(Name = "Date")]
            public string Date { get; set; }
            [DataMember(Name = "Content")]
            public string Content { get; set; }
            [DataMember(Name = "Title")]
            public string Title { get; set; }
        }

        private string getCalories(String content)
        {
            string _uri = "http://nutritionixapi.azurewebsites.net/api/Nutrition/GetCalories";
            HttpWebRequest _request;

            _request = (HttpWebRequest)WebRequest.Create(_uri);
            _request.Method = "POST";
            _request.ContentType = "application/json";

            var json1 = "{\"query\":\""+ content + "\",\"timezone\": \"Europe\"}";
            
            StreamWriter requestWriter = new StreamWriter(_request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(json1);
            requestWriter.Close();

            WebResponse webResponse = _request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            var response = responseReader.ReadToEnd();
            responseReader.Close();

           // var json = JObject.Parse(response);

            //
            return response;
        }

    }
}
