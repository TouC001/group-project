using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
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

        [HttpGet]
        public async Task<IActionResult> FindBooks()
        {
            try
            {
                // Hardcoded search query for software books
                var query = "software";

                // Make an API request to Google Books to search for books
                var googleBooks = await _googleBooksService.GetBooksAsync(query);

                if (googleBooks != null && googleBooks.Count > 0)
                {
                    foreach (var googleBook in googleBooks)
                    {
                        var bookEntity = _bookMappingService.MapToBookEntity(googleBook);
                        _context.Books.Add(bookEntity);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Provide a success message to the user
                    ViewBag.Message = "Books added successfully.";
                }
                else
                {
                    // Provide a message indicating no books were found
                    ViewBag.Message = "No books found.";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, display an error message)
                Console.WriteLine($"Exception: {ex.Message}");

                // Return an error message
                ViewBag.Message = "An error occurred while processing the request.";
            }

            // Return a view with the message
            return View();
        }
    }
}
