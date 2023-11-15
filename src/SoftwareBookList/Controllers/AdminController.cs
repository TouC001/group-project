using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Services;
using SoftwareBookList.Models;

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
        public async Task<IActionResult> PopulateBookData(string keyword)
        {
            try
            {
                // Hardcoded search query for software books
                string query = "software " + keyword;

                // Make an API request to Google Books to search for books
                List<GoogleBook> googleBooks = await _googleBooksService.GetBooksAsync(query);

                if (googleBooks != null && googleBooks.Count > 0)
                {
                    foreach (GoogleBook googleBook in googleBooks)
                    {
                        if (googleBook.VolumeInfo.Title != null &&
                            googleBook.VolumeInfo.ImageLinks != null &&
                            googleBook.VolumeInfo.ImageLinks.Thumbnail != null &&
                            googleBook.VolumeInfo.ImageLinks.SmallThumbnail != null)
                        {
                            string googleId = googleBook.Id;

                            // Check if a book with the same Google ID already exists in the database
                            bool bookExists = await _context.Books.AnyAsync(b => b.GoogleID == googleId);

                            if (!bookExists)
                            {
                                Book bookEntity = _bookMappingService.MapToBookEntity(googleBook);
                                _context.Books.Add(bookEntity);
                            }                         
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

        [Authorize(Roles = "Admin")]
        [Route("update-score")]
        public async Task<IActionResult> PopulateRatings()
        {
            List<int> bookIds = _context.Books.Select(b => b.BookID).ToList();

            foreach (var item in bookIds)
            {
                await _context.RefreshBookRating(item);

            }

            return RedirectToAction(nameof(Index)); 
        }
    }
}
