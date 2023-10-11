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


		// Represents a collection of discussions associated with this user.
		public ICollection<Discussion> Discussions { get; set; }



		// Represents a collection of messages sent by this user.
		public ICollection<Message> SentMessages { get; set; }



		// Represents a collection of messages received by this user.
		public ICollection<Message> ReceivedMessages { get; set; }



		// Represents a collection of book reviews written by this user.
		public ICollection<Review> ReviewsGiven { get; set; }



		// Navigation property for the user's list
		public List List { get; set; }
	}
}