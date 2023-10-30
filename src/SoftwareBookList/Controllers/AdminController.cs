using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Services;

namespace SoftwareBookList.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        private readonly GoogleBooksService _googleBooksService;
        private readonly BookMappingService _bookMappingService;

        public AdminController(DataContext context, GoogleBooksService googleBooksService, BookMappingService bookMappingService)
        {
            _context = context;
            _googleBooksService = googleBooksService;
            _bookMappingService = bookMappingService;
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Ensure only admins can access this action
        public async Task<IActionResult> PopulateBookData()
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
                        if (googleBook.VolumeInfo.Title != null &&
                            googleBook.VolumeInfo.ImageLinks != null &&
                            googleBook.VolumeInfo.ImageLinks.Thumbnail != null &&
                            googleBook.VolumeInfo.ImageLinks.SmallThumbnail != null)
                        {
                            var bookEntity = _bookMappingService.MapToBookEntity(googleBook);
                            _context.Books.Add(bookEntity);
                        }                    
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

            return RedirectToAction("AdminPage"); // Redirect back to the Admin page
        }

    }
}
