using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class Message
    {
        public int MessageID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        // Property to represent the user who sent the message
        public User Sender { get; set; }

        // Property to represent the discussion related to the message
        public Discussion RelatedDiscussion { get; set; }
    }
}
