using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Model_View
{
    public class EditBookViewModel
    {
        public EditBookViewModel()
        {
            
        }

        public EditBookViewModel(int bookID, int ratingValue, int statusID)
        {
            this.BookID = bookID;
            this.RatingValue = ratingValue;
            this.StatusID = statusID;
        }

        [Required(ErrorMessage = "Book entry ID is required")]
        public int BookID { get; set; } // This should represent the book entry's ID

        [Required(ErrorMessage = "Updated Personal Rating is required")]
        public int RatingValue { get; set; } // This should represent the updated rating

        [Required(ErrorMessage = "Updated Status is required")]
        public int StatusID { get; set; } // This should represent the updated status
    }
}
