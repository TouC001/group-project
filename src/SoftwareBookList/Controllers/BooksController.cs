using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoftwareBookList.GoogleBooks;

namespace SoftwareBookList.Controllers
{
	public class BooksController : Controller
    {
        private readonly GoogleBooksService _googleBooksService;
        private readonly GoogleBooksSettings _googleBooksSettings;

        public BooksController(GoogleBooksService googleBooksService, IOptions<GoogleBooksSettings> googleBooksSettings)
        {
            _googleBooksService = googleBooksService;
            _googleBooksSettings = googleBooksSettings.Value; // Check this value during debugging
        }

        [HttpGet]
        public async Task<IActionResult> FindBooks()
        {
            try
            {
                // Hardcoded search query for software books
                var query = "software";

                // Make an API request to Google Books to search for books
                var bookViewModels = await _googleBooksService.GetBooksAsync(query);

                if (bookViewModels != null && bookViewModels.Count > 0)
                {
                    // Debugging output: Check the count of books retrieved
                    Console.WriteLine($"Number of books retrieved: {bookViewModels.Count}");
                    return View("Books", bookViewModels);
                }
                else
                {
                    // Debugging output: Check if there are items in the API response
                    Console.WriteLine("No items found in the API response.");
                    return Content("No books found.", "text/plain");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, display an error message)
                Console.WriteLine($"Exception: {ex.Message}");

                return Content("An error occurred while processing the request.", "text/plain");
            }
        }
    }
}
