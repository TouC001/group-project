namespace SoftwareBookList.Models
{
    public class List
    {
        public int ListID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }

        // Property to represent the user who owns the List
        public User User { get; set; }

        // Property to represent that Books in the BookList
        public ICollection<BookList> BooksInList { get; set; }
    }
}
