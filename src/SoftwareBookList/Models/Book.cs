using System.ComponentModel.DataAnnotations;

namespace SoftwareBookList.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        public string GoogleID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Authors { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ISBN { get; set; }

        public string PublishedDate { get; set; }

        public string ThumbnailLink { get; set; }



        // Navigation property to represent the many-to-many relationship with tags	
        public List<BookTag> BookTags { get; set; }



        // Property to represent the BookList(s) it belongs to
        public List<BookInList> BookInLists { get; set; }



        // One-to-Many Relationship with Review Table
        public List<Review> Reviews { get; set; }




        // One-to-Many Relationship with Discussion Table
        public List<Discussion> Discussions { get; set; }
    }
}