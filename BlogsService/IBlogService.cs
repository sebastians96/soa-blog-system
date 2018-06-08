using BlogDataAccessLayer.Entity;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using System;
using System.Web;

namespace BlogsService
{
    [ServiceContract]
    public interface IBlogService
    {
        //Comments

        [OperationContract, 
            WebInvoke(UriTemplate ="/GetAllComments", Method ="GET", BodyStyle =WebMessageBodyStyle.Wrapped, 
            RequestFormat =WebMessageFormat.Json, ResponseFormat =WebMessageFormat.Json)]
        List<Comment> GetAllComments();

        [OperationContract,
            WebInvoke(UriTemplate = "/GetComment?id={id}", Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Comment GetCommentById(int id);

        [OperationContract,
             WebInvoke(UriTemplate = "/AddComment", 
            Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddComment(BlogService.CommentWCF comment);

        [OperationContract,
             WebInvoke(UriTemplate = "/DeleteComment?id={id}", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Comment DeleteComment(int id);

        // Posts

        //[OperationContract]
        //List<Post> GetAllPosts();

        //[OperationContract]
        //Post GetPostById(int Id);

        //[OperationContract]
        //void AddPost(Post post);

        //[OperationContract]
        //Post UpdatePost(int Id);

        //[OperationContract]
        //void DeletePost(int Id);
    }
}
