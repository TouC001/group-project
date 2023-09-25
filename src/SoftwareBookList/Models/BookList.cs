namespace SoftwareBookList.Models
{
    public class BookList
    {
        public int BookListID { get; set; }

        // Status property to represent the status of the BookList
        public BookListStatus BookListStatus { get; set; }


        // Navigation property to represent the User who owns this list
        public User User { get; set; }


        // Navigation property to represent the books in this list
        public ICollection<Book> Books { get; set; }
    }
}
