using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

        public async Task<List<GoogleBook>> GetBooksAsync(string query)
        {
			try
			{
				var apiKey = _googleBooksSettings.ApiKey;
				var requestUrl = $"volumes?q={query}&maxResults=40&key={apiKey}";

				var response = await _httpClient.GetAsync(requestUrl);

				response.EnsureSuccessStatusCode(); // Ensure HTTP success status

				List<GoogleBook> googleBooks = ParseAndTransformResponse(await response.Content.ReadAsStringAsync());

				return googleBooks;
			}
			catch (HttpRequestException ex)
			{
				// Handle exceptions, possibly log them
				return null;
			}
        }

		private List<GoogleBook> ParseAndTransformResponse(string responseContent)
		{
			// Parse and transform the API response here
			// This logic can be extracted to a separate method or class
			var googleBooks = new List<GoogleBook>();

            try
            {
				var apiResponse = JsonConvert.DeserializeObject<GoogleBooksAPIResponse>(responseContent);

				if (apiResponse != null && apiResponse.Items != null)
				{
					foreach (var item in apiResponse.Items)
					{
						var googleBook = new GoogleBook
						{
							
						};
					}
				}
			}
			catch (JsonException ex)
            {
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
            }

			return googleBooks;
		}
	}
}