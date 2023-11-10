using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Net;
using System.Security.Claims;

namespace SoftwareBookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        private readonly GoogleBooksService _googleBooksService;
        private readonly BookMappingService _bookMappingService;
        private readonly AddBooksService _addBooksServicee;

        public BooksController(DataContext context, GoogleBooksService googleBooksService, BookMappingService bookMappingService)
        {
            _context = context;
            _googleBooksService = googleBooksService;
            _bookMappingService = bookMappingService;
            _addBooksServicee = new AddBooksService(context);
        }

        public IActionResult Books()
        {
            List<Book> books = _context.Books.ToList();
            return View(books);
        }

        public async Task<IActionResult> BookDetails(string googleID)
        {
            GoogleBook googleBook = await _googleBooksService.GetSingleBookAsync(googleID);

            if (googleBook == null)
            {
                return NotFound();
            }

            return View(googleBook);
        }

<<<<<<< HEAD
        // Gets the rating scores of the BookInList of the user
        public double GetBookInListRating()
        {

        }

        // Gets the rating scores of the users review
        public double GetReviewRating()
        {

        }

        // Gets the total rating score from the review and BooksInList
        public IActionResult GetBookTotalRating()
        {
            
=======
        [HttpPost("AddToList")]
        public IActionResult AddBook(AddBookViewModel addBookViewModel)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }


            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int bookListID = _addBooksServicee.GetBookListIDForUser(userID);

                if (bookListID != 0)
                {
                    bool IsBookAlreadAdded = _addBooksServicee.CheckIfBookIsAdded(bookListID, addBookViewModel.BookID);

                    if (IsBookAlreadAdded)
                    {
                        TempData["ErrorMessage"] = "Already Added.";
                    }
                    else
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

                            _context.SaveChanges();

                            TempData["SuccessMessage"] = "Book added to your list successfully.";

                            return RedirectToAction("Books");
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }

                }
            }
            return RedirectToAction("Books");
>>>>>>> 0e943a910c5a3567b2127741fcaffb98242a5d37
        }
    }
}
