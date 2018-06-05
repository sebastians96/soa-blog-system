
using System.Data.Entity;
using BlogDataAccessLayer.Entity;
using BlogDataAccessLayer.Initializer;

namespace BlogDataAccessLayer.Context
{
    /// <summary>
    ///  Context of blog database
    /// </summary>
    public class BlogContext : DbContext, IBlogContext
    {
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BlogContext() : base("Server=LENOVO-PC\\SIENMICH;Database=blog;User Id=root;Password=root;")
        {
            Database.SetInitializer<BlogContext>(new BlogInitializer());
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        /// <summary>
        /// Creating relationships
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasRequired(m => m.Post);

            base.OnModelCreating(modelBuilder);
        }


    }
}
