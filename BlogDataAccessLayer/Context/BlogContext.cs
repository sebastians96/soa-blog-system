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
        /// Constructor with connection string passed to base context
        /// </summary>
        public BlogContext() : base(@"Server=DESKTOP-H6URO1D\SQLEXPRESS;Database=blog;User Id=root;Password=root;")
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
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Function that saves context
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
