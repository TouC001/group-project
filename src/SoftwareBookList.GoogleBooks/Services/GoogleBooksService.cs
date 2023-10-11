using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SoftwareBookList;
using SoftwareBookList.GoogleBooks;

namespace SoftwareBookList.GoogleBooks
{
    /// <summary>
    /// This class takes an instance of HttpClient through dependency injection
    /// and configures it with the base URL for the Google Books API.
    /// </summary>
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleBooksSettings _googleBooksSettings;

        public GoogleBooksService(HttpClient httpClient, IOptions<GoogleBooksSettings> googleBooksSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _googleBooksSettings = googleBooksSettings.Value;
            _httpClient.BaseAddress = new Uri("https://www.googleapis.com/books/v1/"); // Base URL for Google Books API
        }

        public async Task<List<BookViewModel>> GetBooksAsync(string query)
        {
            try
            {
                var apiKey = _googleBooksSettings.ApiKey;
                var requestUrl = $"volumes?q={query}&maxResults=40&key={apiKey}";

                var response = await _httpClient.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode(); // Ensure HTTP success status

                var result = await response.Content.ReadAsStringAsync();
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
                }

                return bookViewModels;
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception (e.g., log it, display an error message)
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                return null;
            }
        }
    }
}