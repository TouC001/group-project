namespace SoftwareBookList.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        // Navigation property to represent the many-to-many relationship with books
        // ICollection just represents a collection of objects. So this represents a collection of Booktags.
        public ICollection<BookTag> BookTags { get; set; }
    }
}
