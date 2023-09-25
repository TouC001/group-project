using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int RatingValue { get; set; }

        // This is representing the User who provied the rating. It is referncing the User class.
        // Could be replaced with using the UserID, but I am unsure.
        public User User { get; set; }

        // This is representing the book that has been given a rating. It is referencing the Book class.
        // Could be replaced with using the BookID, but I am unsure.
        public Book RatedBook { get; set; }
    }
}
