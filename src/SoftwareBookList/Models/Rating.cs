using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class Rating
	{
		[Key]
		public int RatingID { get; set; }

		[Required]
		[Range(1.0, 10.0, ErrorMessage = "RatingValue must be between 1 and 10.")]
		// This makes it so, the rating can be a whole or decimal value.
		public double RatingValue { get; set; }


		// Navigation property for the one-to-many relationship with Review
		public List<Review> Reviews { get; set; }

	}
}
