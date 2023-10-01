using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class List
	{
		[Key]
		public int ListID { get; set; }

		[ForeignKey("User")]
		public int UserID { get; set; }

		public string Name { get; set; }


		// Navigation property for the associated user
		public User User { get; set; }


		// Property to represent that Books in the BookList
		public ICollection<BookList> BooksInList { get; set; }
	}
}
