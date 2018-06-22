using Authentication_Service.Models;
using BlogDataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class CompoundAdminView
    {
        public List<Post> Posts { get; set; }
        public User User { get; set; }
    }
}