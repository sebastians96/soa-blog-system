using BlogDataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class CompoundBlogView
    {
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}