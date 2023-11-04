using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class UserProfileViewModel
    {
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        [StringLength(200, ErrorMessage = "User Bio must be 200 charactres or less.")]
        public string Bio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public BookList BooksInList { get; set; }

        public int UserID { get; set; }
    }
}
