using BlogDataAccessLayer.Context;
using BlogDataAccessLayer.Entity;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace BlogDataAccessLayer.Initializer
{
    /// <summary>
    /// Database initializer for blog db. 
    /// </summary>
    class BlogInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        public override void InitializeDatabase(BlogContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        /// <summary>
        /// Seed db with basic data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(BlogContext context)
        {

            var post1 = new Post()
            {
                PostID = 1,
                Content = "for breakfast i ate 2 eggs, 2 slice of bacon and 200g bread. Counted calories:782.64",
                Date = "2018-07-08",
                User = "KacpSzalw",
                Title = "Breakfast - Kacper Szalwa"
            };

            var comment1 = new Comment()
            {
                CommentID = 1,
                Content = "Kacper good job but you should eat also 250g chicken. Counted calories:550",
                Date = "2018-07-09",
                User = "SienMich",
                PostID = 1
            };

            context.Posts.Add(post1);
            context.Comments.Add(comment1);
            base.Seed(context);
        }
    }
}
