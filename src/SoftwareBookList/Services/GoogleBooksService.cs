namespace SoftwareBookList.Services
{
    /// <summary>
    /// This class takes an instance of HttpClient through dependency injection
    /// and configures it with the base URL for the Google Books API.
    /// </summary>
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;

        public GoogleBooksService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://www.googleapis.com/books/v1/"); // Base URL for Google Books API
        }

        /// <summary>
        /// This is where we make the GET request for books based on a search term we give.
        /// We also are setting the maxResults to 40 to get 40 books back.
        /// </summary>
        /// <param name="query">This is the search term we are using when we search Google Books.</param>
        /// <returns></returns>
        public async Task<string> GetBooksAsync(string query)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"volumes?q={query}&maxResults=40");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle API error responses here
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return null;
            }
        }
    }
}
