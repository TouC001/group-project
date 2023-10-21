using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoftwareBookList.GoogleBooks;

namespace SoftwareBookList.Controllers
{
	public class BooksController : Controller
    {
        private readonly GoogleBooksService _googleBooksService;

        public BooksController(GoogleBooksService googleBooksService)
        {
            _googleBooksService = googleBooksService;
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
                    return View("Books", bookViewModels);
                }
                else
                {
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
