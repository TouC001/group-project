using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class Review
	{
		[Key]
		public int ReviewID { get; set; }

		[ForeignKey("User")]
		public int UserID { get; set; }

		[ForeignKey("Book")]
		public int BookID { get; set; }

		public int RatingValue { get; set; }


		[Required]
		[MaxLength(1000)]
		public string ReviewText { get; set; }



		// Navigation property for the one-to-many relationship with User
		public User User { get; set; }



		// Represents the Book that the review is about
		public Book Book { get; set; }
	}
}