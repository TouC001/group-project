using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class UserProfileViewModel
    {
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string UserName { get; set; }

        public string? ProfilePicture { get; set; }

        [StringLength(30000, ErrorMessage = "User Bio must be 30000 characters or less.")]
        public string Bio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; }

        public BookList? UserBookList { get; set; }

        public DateTime DateJoin { get; set; }

        public int UserID { get; set; }
    }
}
