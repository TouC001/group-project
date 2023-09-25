namespace SoftwareBookList.Models
{
    public class BookTag
    {
        public int BookID { get; set; }
        public int TagId { get; set; }

        // Navigation properties to link to the related Book and Tag
        public Book Book { get; set; }
        public Tag Tag { get; set; }
    }
}
