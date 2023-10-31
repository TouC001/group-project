using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class Discussion
	{
		[Key]
		public int DiscussionID { get; set; }

		[ForeignKey("User")]
		public int UserID { get; set; }

		[ForeignKey("Book")]
		public int BookID { get; set; }

		[Required]
		[StringLength(255)]
		public string Title { get; set; }

		[Required]
		[StringLength(1000)]
		public string Content { get; set; }


		public DateTime CreatedAt { get; set; }


		// Property to represent the user who created the discussion
		public User Creator { get; set; }



		// Property to represent the book associated with the discussion
		public Book AssociatedBook { get; set; }



		// Navigation property for the one-to-many relationship with Message
		public List<Message> Messages { get; set; }



		// Navigation property for the one-to-many relationship with User
		public User User { get; set; }
	}
}