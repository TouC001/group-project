using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
	public class BookListStatus
	{
		[Key]
		public int StatusID { get; set; }

		[Required]
		public string StatusName { get; set; }


		// Navigation property for associated BookList entities
		public ICollection<BookList> BookLists { get; set; }
	}
}