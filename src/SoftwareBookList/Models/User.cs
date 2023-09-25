using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [Url]
        public string ProfilePictureUrl { get; set; }

        // Property to represent the user's list
        public List List { get; set; }

        // This will just be for later if we want to added these in.
        // public int PostCount { get; set; }
        // public int CommentCount { get; set; }
        // public string FacebookProfile { get; set; }
        // public string TwitterProfile { get; set; }
    }
}