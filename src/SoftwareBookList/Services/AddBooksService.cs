using SoftwareBookList.Data;
using SoftwareBookList.Models;

namespace SoftwareBookList.Services
{
            public class AddBooksService
            {
                private readonly DataContext _dataContext; 


                public AddBooksService(DataContext dataContext)
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

                public bool CheckIfBookIsAdded(int bookListID, int bookID)
                {
                    return _dataContext.BookInLists
                         .Any(bil => bil.BookID == bookID && bil.BookListID == bookListID);
                }
            }
}

