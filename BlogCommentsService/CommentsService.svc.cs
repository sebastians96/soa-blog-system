using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BlogDataAccessLayer.Entity;
using BlogDataAccessLayer.Context;

namespace BlogCommentsService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BlogCommentsService : ICommentsService
    {
        private readonly IBlogContext _blogContext;

        public BlogCommentsService()
        {
            this._blogContext = new BlogContext();
        }

        public void AddComment(Comment comment)
        {
            _blogContext.Comments.Add(comment);
            _blogContext.SaveChanges();
        }

        public List<Comment> GetAllComments()
        {
            return _blogContext.Comments.ToList();
        }

        public Comment GetCommentById(int Id)
        {
            return _blogContext.Comments.ToList().Find(c => c.Id == Id);
        }

        public void DeleteComment(int Id)
        {
            throw new NotImplementedException();
        }

        public Comment UpdateComment(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
