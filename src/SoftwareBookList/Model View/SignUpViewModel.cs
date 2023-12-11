using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "*This field is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "*This field is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "*This field is required")]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "*This field is required")]
		[DataType(DataType.Password)]
		[StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
		public string Password { get; set; }

		[Required(ErrorMessage = "*This field is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string PasswordConfirm { get; set; }
	}
}
