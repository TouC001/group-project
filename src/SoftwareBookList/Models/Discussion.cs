using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class Discussion
    {
        public int DiscussionID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        //Property to represent the user who created the discussion
        public User Creator { get; set; }

        // Property to represent replies or posts within the discussion
        public List<Post> Posts { get; set; }

        // Property to represent the book associated with the discussion
        public Book AssociatedBook { get; set; }
    }
}