using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
	public class UserAccount
	{
		[Key]
		public int AccountID { get; set; }

		[ForeignKey("User")]
		public int UserID { get; set; }

		public string? UserName { get; set; }

		public string? ProfilePicture { get; set; }

		[StringLength(30000)]
		public string? Bio {  get; set; }

		[DataType(DataType.Date)]
		public DateTime? Birthday { get; set; }

		public string? EmailAddress { get; set; }

		public User User { get; set; }

	}
}
