using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftwareBookList.GoogleBooks;

namespace SoftwareBookList.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        public string GoogleID { get; set; }
        [Required]
        public string SmallThumbnail { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        [Required]
        public string Title { get; set; }
        [NotMapped]
        public string Subtitle { get; set; }
        [NotMapped]
        public List<string> Authors { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public string PublishedDate { get; set; }
        [NotMapped]
        public string SelfLink { get; set; }
        [NotMapped]
        public string Publisher { get; set; }
        [NotMapped]
        public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
        [NotMapped]
        public List<string> Categories { get; set; }

        public double DbTotalScore {  get; set; }

        // Navigation property to represent the many-to-many relationship with tags	
        public List<BookTag> BookTags { get; set; }

        // Property to represent the BookList(s) it belongs to
        public List<BookInList> BookInLists { get; set; }

        // One-to-Many Relationship with Review Table
        public List<Review> Reviews { get; set; }

        public List<Comment> Comments { get; set; }

        // One-to-Many Relationship with Discussion Table
        public List<Discussion> Discussions { get; set; }

        // This is the computer rating average. This is getting average of all ratings combined.
        public double ComputerRating()
        {
            List<int> ratings = new List<int>();

            // That is putting all the book in list ratings into that list.
            ratings.AddRange(BookInLists.Where(b => b.RatingValue != 0).Select(b => b.RatingValue));

            // This is putting the ratings from the book in list and appends the ratings from the reviews.
            ratings.AddRange(Reviews.Where(b => b.RatingValue != 0).Select(r => r.RatingValue));

            // if the rating count is 0 for a book, we will place it at the end.
            if (ratings.Count > 0)
            {
                // return ratings.Average();

                double averageRating = ratings.Average();

                // Rounds the average rating to one decimal.
                return Math.Round(averageRating, 1);
            }
            else
            {
                return 0;
            }
        }

        // Getting the total number of ratings 
        public float PossibleNumberRating()
        {
            return Math.Max(1, Reviews.Count + BookInLists.Count);
        }

        // Gives us the rating or score based on average rating compared against TotalNumberOfRatings.
        public double TotalScore()
        {
            // returns the correct average.
            return ComputerRating();
        }

        public double NumberRating()
        {
            return BookInLists.Count;
        }
    }
}



        