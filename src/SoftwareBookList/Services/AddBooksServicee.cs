using SoftwareBookList.Data;
using SoftwareBookList.Models;

namespace SoftwareBookList.Services
{
    public class AddBooksServicee
    {
        private readonly DataContext _dataContext; 


        public AddBooksServicee(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public BookInList AddBookToUserList(BookInList bookInList)
        {
            // Add the book to the user's list (BookInList)
            _dataContext.BookInLists.Add(bookInList);
            _dataContext.SaveChanges();
            return bookInList;
        }

        public int GetBookListIDForUser(int userID)
        {
            var bookList = _dataContext.BookLists.FirstOrDefault(bl => bl.UserID == userID);

            if (bookList != null)
            {
                return bookList.BookListID;
            }

            return 0;
        }

        public List<BookListStatus> GetStatusOptions()
        {
            // Retrieve all available status options from the database
            return _dataContext.BookListStatus.ToList();
        }

        public Book GetBookByBookID(int bookID)
        {
            return _dataContext.Books.FirstOrDefault(book => book.BookID == bookID);
        }

        public BookListStatus GetStatusForBook(int bookID)
        {
            var bookInList = _dataContext.BookInLists.FirstOrDefault(b => b.BookID == bookID);
            return bookInList?.Status;
        }
    }
}

