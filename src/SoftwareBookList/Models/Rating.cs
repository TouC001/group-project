using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class Rating
	{
		[Key]
		public int RatingID { get; set; }

		[Required]
		[Range(1, 10, ErrorMessage = "RatingValue must be between 1 and 10.")]
		public int RatingValue { get; set; }


		// Navigation property for the one-to-many relationship with Review
		public List<Review> Reviews { get; set; }

	}
}
