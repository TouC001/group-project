using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class User
	{
		[Key]
		public int UserID { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
		public bool IsAdmin { get; set; }

		[Required]
		public DateTime DateJoin { get; set; }

		// Represents a collection of discussions associated with this user.
		public List<Discussion> Discussions { get; set; }

		// Represents a collection of messages sent by this user.
		public List<Message> SentMessages { get; set; }

		// Represents a collection of messages received by this user.
		public List<Message> ReceivedMessages { get; set; }

		// Represents a collection of book reviews written by this user.
		public List<Review> ReviewsGiven { get; set; }


		// Represents a collection of Accounts a User can Create.
		public List<UserAccount> UserAccounts { get; set; }

		public List<Comment> UserComment { get; set; }

		// Navigation property for the user's BookList
		public BookList BookList { get; set; }
	}
}