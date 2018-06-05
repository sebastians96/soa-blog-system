using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BlogDataAccessLayer.Entity;
using BlogDataAccessLayer.Context;

namespace BlogPostsService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BlogPostsService : IPostsService
    {
        private readonly IBlogContext _blogContext;

        public BlogPostsService()
        {
            this._blogContext = new BlogContext();
        }

        public void AddPost(Post post)
        {
            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
        }

        public void DeletePost(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            return _blogContext.Posts.ToList();
        }

        public Post GetPostById(int Id)
        {
            return _blogContext.Posts.ToList().Find(c => c.Id == Id);
        }

        public Post UpdatePost(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
