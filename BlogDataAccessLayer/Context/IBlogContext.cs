
using BlogDataAccessLayer.Entity;
using System.Data.Entity;


namespace BlogDataAccessLayer.Context
{
    public interface IBlogContext
    {

        DbSet<Comment> Comments { get; set; }
        DbSet<Post> Posts { get; set; }

        int SaveChanges();
    }
}
