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