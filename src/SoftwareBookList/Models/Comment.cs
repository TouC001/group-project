using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property for the one-to-one relationship with User
        public User Commentor { get; set; }

        // Property to represent the book associated with the comment
        public Book CommentedBook { get; set; }
    }
}