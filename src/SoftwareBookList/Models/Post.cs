using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class Post
    {
        public int PostID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        // Property to represent the user who posted the reply
        public User User { get; set; }

        // Property to represent the discussion this post belongs to
        public Discussion Discussion { get; set; }
    }
}
