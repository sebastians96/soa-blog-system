using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlogDataAccessLayer.Entity
{
    [Serializable]
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Must be between 1 and 50 characters long", MinimumLength = 1)]
        public string User { get; set; }

        [StringLength(100, ErrorMessage = "Must be between 1 and 100 characters long", MinimumLength = 1)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }
    }
}
