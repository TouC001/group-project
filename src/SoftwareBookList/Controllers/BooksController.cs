using Microsoft.AspNetCore.Mvc;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Models;
using SoftwareBookList.Services;

namespace SoftwareBookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        private readonly GoogleBooksService _googleBooksService;
        private readonly BookMappingService _bookMappingService;

        public BooksController(DataContext context, GoogleBooksService googleBooksService, BookMappingService bookMappingService)
        {
            _context = context;
            _googleBooksService = googleBooksService;
            _bookMappingService = bookMappingService;
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
    }
}
