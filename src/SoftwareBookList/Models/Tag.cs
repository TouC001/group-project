using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class Tag
	{
		[Key] // Marks TagId as the primary key
		public int TagId { get; set; }

		[Required]
		[StringLength(255)]
		public string TagName { get; set; }



		// Navigation property to represent the many-to-many relationship with books
		public ICollection<BookTag> BookTags { get; set; }
	}
}
