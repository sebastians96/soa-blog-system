using BlogDataAccessLayer.Entity;
using System.Data.Entity;


namespace BlogDataAccessLayer.Context
{
    /// <summary>
    /// Interface for BlogContext with defined DbSets
    /// </summary>
    public interface IBlogContext
    {

        DbSet<Comment> Comments { get; set; }
        DbSet<Post> Posts { get; set; }

        int SaveChanges();
    }
}
