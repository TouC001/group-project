using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class Message
	{
		[Key]
		public int MessageID { get; set; }

		[ForeignKey("Discussion")]
		public int DiscussionID { get; set; }

		[ForeignKey("User")]
		public int UserID { get; set; }

		[Required]
		[StringLength(1000)]
		public string Content { get; set; }

		public DateTime SentAt { get; set; }



		// ID of the user who sent the message -- This acts as the FK because it relates to the UserID who sent the message.
		public int SenderID { get; set; }



		// ID of the user who is the recipient of the message -- This acts as the FK because it relates to the UserID who received the message.
		public int RecipientID { get; set; }



		// Navigation property for the sender
		public User Sender { get; set; }



		// Navigation property for the recipient
		public User Recipient { get; set; }


		// Navigation property for the discussion
		public Discussion Discussion { get; set; }

	}
}
