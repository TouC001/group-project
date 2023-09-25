namespace SoftwareBookList.Models
{
    public class Book
    {
        public string GoogleID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string PublishedDate { get; set; }
        public string ThumbnailLink { get; set; }

        // Add this property to represent the many-to-many relationship with tags
        // Property to represent the many-to-many relationship with tags
        public ICollection<Tag> Tags { get; set; }

        // Property to represent the BookList(s) it belongs to
        public ICollection<BookList> BookLists { get; set; }
    }
}
