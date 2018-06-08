using BlogDataAccessLayer.Context;
using BlogDataAccessLayer.Entity;
using System.Data.Entity;
using System;

namespace BlogDataAccessLayer.Initializer
{
    class BlogInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        public override void InitializeDatabase(BlogContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(BlogContext context)
        {

            var post1 = new Post()
            {
                Id = 1,
                Content = "Jestem dzik i jem ile moge",
                Date = new DateTime(2018, 6, 5),
                User = "KacpSzalw", 
                Title = "Odżywianie - Kacper Szalwa"
            };
            context.Posts.Add(post1);

            var comment1 = new Comment()
            {
                Id = 1,
                Content = "Kacper dobra robota!",
                Date = new DateTime(2018, 6, 5),
                Post = post1,
                User = "SienMich"
            };
            context.Comments.Add(comment1);

            base.Seed(context);
        }
    }
}
