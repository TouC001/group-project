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
        private readonly AddBooksServicee _addBooksServicee;

        public BooksController(DataContext context, GoogleBooksService googleBooksService, BookMappingService bookMappingService)
        {
            _context = context;
            _googleBooksService = googleBooksService;
            _bookMappingService = bookMappingService;
            _addBooksServicee = new AddBooksServicee(context);
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

        [HttpPost("AddToList")]
        public IActionResult AddBook(AddBookViewModel addBookViewModel)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }


            if(User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int bookListID = _addBooksServicee.GetBookListIDForUser(userID);

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

                        _context.SaveChanges();

                        return RedirectToAction("Books");
                    }
                    catch (Exception ex)
                    {
                        return NotFound();
                    }
                }
            }
            return RedirectToAction("Login");
        }
    }
}
