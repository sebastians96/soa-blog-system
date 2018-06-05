using BlogDataAccessLayer.Entity;
using System.Collections.Generic;
using System.ServiceModel;

namespace BlogPostsService
{
    [ServiceContract]
    public interface IPostsService
    {
        [OperationContract]
        List<Post> GetAllPosts();

        [OperationContract]
        Post GetPostById(int Id);

        [OperationContract]
        void AddPost(Post post);

        [OperationContract]
        Post UpdatePost(int Id);

        [OperationContract]
        void DeletePost(int Id);
    }
}
