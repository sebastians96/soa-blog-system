using BlogDataAccessLayer.Entity;
using System.Collections.Generic;
using System.ServiceModel;

namespace BlogCommentsService
{
    [ServiceContract]
    public interface ICommentsService
    {

        [OperationContract]
        List<Comment> GetAllComments();

        [OperationContract]
        Comment GetCommentById(int Id);

        [OperationContract]
        void AddComment(Comment post);

        [OperationContract]
        Comment UpdateComment(int Id);

        [OperationContract]
        void DeleteComment(int Id);
    }
}
