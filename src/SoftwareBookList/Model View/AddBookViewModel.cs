using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            
        }

        public AddBookViewModel(int bookID, int statusID, int ratingValue)
        {
            this.BookID = bookID;
            this.StatusID = statusID;
            this.RatingValue = ratingValue;
        }

        [Required]
        public int BookID { get; set; } // This should represent the selected book's ID

        [Required(ErrorMessage = "Status is required")]
        public int StatusID { get; set; } // This should represent the selected status

        [Required(ErrorMessage = "Personal Rating is required")]
        public int RatingValue { get; set; } // This should represent the selected rating
    }
}
