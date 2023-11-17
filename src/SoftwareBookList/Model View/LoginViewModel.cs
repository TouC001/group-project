using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "*This field is required")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "*This field is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
