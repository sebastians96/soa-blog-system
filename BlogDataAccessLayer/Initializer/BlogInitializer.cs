using BlogDataAccessLayer.Context;
using BlogDataAccessLayer.Entity;
using System.Data.Entity;
using System;
using System.Collections.Generic;

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
                PostID = 1,
                Content = "Jestem dzik i jem ile moge",
                Date = "2018-07-08",
                User = "KacpSzalw",
                Title = "Odżywianie - Kacper Szalwa"
            };

            var comment1 = new Comment()
            {
                CommentID = 1,
                Content = "Kacper dobra robota!",
                Date = "2018-07-08",
                User = "SienMich",
                PostID = 1
            };

            context.Posts.Add(post1);
            context.Comments.Add(comment1);
            base.Seed(context);
        }
    }
}
