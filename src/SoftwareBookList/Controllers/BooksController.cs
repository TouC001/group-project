using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            // Hardcoded search query for software books
            var query = "software";

            // Make an API request to Google Books to search for books
            var result = await _googleBooksService.GetBooksAsync(query);

            if (result != null)
            {
                // Deserialize the JSON response into the GoogleBooksApiResponse class
                var apiResponse = JsonConvert.DeserializeObject<GoogleBooksApiResponse>(result);

                var bookViewModels = new List<BookViewModel>();

                if (apiResponse != null && apiResponse.Items != null)
                {
                    foreach (var item in apiResponse.Items)
                    {
                        var bookViewModel = new BookViewModel
                        {
                            Title = item.VolumeInfo.Title,
                            Authors = string.Join(", ", item.VolumeInfo.Authors),
                            Description = item.VolumeInfo.Description,
                            PublishedDate = item.VolumeInfo.PublishedDate
                            // Map other properties as needed
                        };

                        bookViewModels.Add(bookViewModel);
                    }

                    // Debugging output: Check the count of books retrieved
                    Console.WriteLine($"Number of books retrieved: {bookViewModels.Count}");
                }
                else
                {
                    // Debugging output: Check if there are items in the API response
                    Console.WriteLine("No items found in the API response.");
                }

                return View("Books", bookViewModels);
            }

            // Debugging output: Check if the API request was successful
            Console.WriteLine("API request failed or returned null result.");
            return Content(result, "application/json"); // For demonstration, returning JSON directly
        }

    }
}
