namespace SoftwareBookList.Models
{
    public class Book
    {
        public string GoogleID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string Author { get; set; }
        public decimal Rating { get; set; }
    }
}
