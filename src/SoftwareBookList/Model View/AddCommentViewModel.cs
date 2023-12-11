using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Model_View
{
    public class AddCommentViewModel
    {
        public int UserID { get; set; }

        public string BookID { get; set; }

        [Required(ErrorMessage = "Please enter a comment.")]
        [StringLength(5000, ErrorMessage = "The comment cannot exceed 5000 characters.")]
        public string textContent { get; set; }
    }
}
