using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Model_View;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SoftwareBookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        private readonly GoogleBooksService _googleBooksService;
        private readonly BookMappingService _bookMappingService;
        private readonly AddBooksService _addBooksService;

        public BooksController(DataContext context, GoogleBooksService googleBooksService, BookMappingService bookMappingService, AddBooksService addBooksService)
        {
            _context = context;
            _googleBooksService = googleBooksService;
            _bookMappingService = bookMappingService;
            _addBooksService = addBooksService;
        }

        public bool CheckIfBookIsAdded(int bookID, int bookListID)
        {
            // Check if a record with the given bookID and bookListID exists in the BookInList table
            bool isAdded = _context.BookInLists.Any(bil => bil.BookID == bookID && bil.BookListID == bookListID);

            return isAdded;
        }

        public async Task<IActionResult> Books(int page = 1)
        {

            int pageSize = 30; // Display 30 books per page

            Dictionary<int, double> userScore = new Dictionary<int, double>();

            Stopwatch sw1 = Stopwatch.StartNew();
            // Stopwatch took 1.6 seconds here
            IQueryable<Book> allBooksQuery = _context.Books.Include(b => b.BookInLists).OrderByDescending(b => b.DbTotalScore);

            sw1.Stop();
            Console.WriteLine(sw1.Elapsed.TotalSeconds);
            Stopwatch sw2 = Stopwatch.StartNew();
            // Create a paginated list
            BookPaginatedList<Book> paginatedList = new BookPaginatedList<Book>(allBooksQuery, page, pageSize);
            sw2.Stop();
            Console.WriteLine(sw2.Elapsed.TotalSeconds);


            // Get the list of books for the current page
            List<Book> books = paginatedList.Books.ToList();



            // Creating a Dictionary to store whether each book is already added
            Dictionary<int, bool> bookAlreadyAddedMap = new Dictionary<int, bool>();

            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                int bookListID = await _addBooksService.GetBookListIDForUser(userID);
                List<BookInList> bookInList = _context.BookInLists.Where(x => x.BookListID == bookListID).ToList();

                List<int> addedBookIds = bookInList.Select(x => x.BookID).Distinct().ToList();

                // Loop through the list of books and check if each one is already added to the user's list
                foreach (var book in books)
                {
                    int bookID = book.BookID;

                    bool isBookAlreadyAdded = addedBookIds.Contains(bookID);

                    // Store the result in the dictionary, using the bookID as the key
                    bookAlreadyAddedMap[bookID] = isBookAlreadyAdded;

                    var userBookInList = bookInList.FirstOrDefault(x => x.BookID == bookID);

                    if (userBookInList != null)
                    {
                        userScore.Add(bookID, userBookInList.RatingValue);
                    }
                }
            }

            // grabs the user score. 
            ViewData["UserScore"] = userScore;

            // Pass the dictionary to the view using ViewData, so it can be available in the Razor view
            ViewData["BookAlreadyAddedMap"] = bookAlreadyAddedMap;

            // Return the paginated list to the view
            return View(paginatedList);
        }

        public async Task<IActionResult> BookDetails(string googleID)
        {
            int UserID = 0;

            if (User.Identity.IsAuthenticated)
            {
                UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            }

            GoogleBook googleBook = await _googleBooksService.GetSingleBookAsync(googleID);

            if (googleBook == null)
            {
                return NotFound();
            }

            return View(new Book_CommentViewMmodel() { googleBook = googleBook, addComment = new() { UserID = UserID, BookID = googleBook.Id}, comments = _context.GetCommentForBooks(googleID) });
        }

        [HttpPost("AddToList")]
        public async Task<IActionResult> AddBook(AddBookViewModel addBookViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(addBookViewModel);
            }


            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int bookListID = await _addBooksService.GetBookListIDForUser(userID);

                if (bookListID != 0)
                {

                    BookInList bookInList = new BookInList
                (
                    addBookViewModel.BookID,
                    addBookViewModel.StatusID,
                    bookListID,
                    addBookViewModel.RatingValue
                );

                    try
                    {
                        _context.BookInLists.Add(bookInList);

                        await _context.SaveChangesAsync();

                        await _context.RefreshBookRating(bookInList.BookID);

                        return RedirectToAction("Books");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Books");
                    }

                }
            }
            return RedirectToAction("Books");
        }

        [HttpPost("EditBook")]
        public async Task<IActionResult> EditBook(EditBookViewModel editBookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editBookViewModel);
            }

            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                int bookListID = await _addBooksService.GetBookListIDForUser(userID);

                // Check if the user's profile is fully updated
                if (bookListID == 0)
                {
                    // Return an error message if the user's profile is not fully updated
                    ModelState.AddModelError("", "User's profile is not fully updated.");
                    return View(editBookViewModel);
                }

                BookInList oldBookInList = await _context.BookInLists.FirstOrDefaultAsync(bil => bil.BookID == editBookViewModel.BookID && bil.BookListID == bookListID);

                if (oldBookInList != null)
                {
                    //Creating a New BookInList Object with updated Status and RatingValue
                    BookInList newBookInList = new BookInList
                    (
                        oldBookInList.BookID,
                        editBookViewModel.StatusID,
                        bookListID,
                        editBookViewModel.RatingValue
                    );

                    _context.Entry(oldBookInList).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();

                    _context.BookInLists.Add(newBookInList);
                    await _context.SaveChangesAsync();

                    await _context.RefreshBookRating(newBookInList.BookID);

                    return Redirect("Account");
                }
            }

            return View(editBookViewModel);
        }


        private async Task DeleteOldBookInList(BookInList oldBookInList)
        {
            _context.BookInLists.Remove(oldBookInList);

            await _context.SaveChangesAsync();
        }


        [HttpPost("RemoveBook")]
        public async Task<IActionResult> RemoveBook(int bookID)
        {
            if (!ModelState.IsValid)
            {
                return View("Account");
            }

            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int bookListID = await _addBooksService.GetBookListIDForUser(userID);

                // Find the book in the user's list
                BookInList bookInList = await _context.BookInLists.FirstOrDefaultAsync(bil => bil.BookID == bookID && bil.BookListID == bookListID);


                if (bookInList != null)
                {
                    _context.BookInLists.Remove(bookInList);

                    await _context.SaveChangesAsync();

                    await _context.RefreshBookRating(bookInList.BookID);

                    return Redirect("Account");
                }
            }

            return View("Account");

        }
    }
}
