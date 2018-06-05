using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogDataAccessLayer.Entity
{
    [Serializable]
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Must be between 1 and 50 characters long", MinimumLength = 1)]
        public string User { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        [ForeignKey("Post")]
        public Post Post { get; set; }
        
    }
}
