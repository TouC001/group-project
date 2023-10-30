using SoftwareBookList.Models;
using SoftwareBookList.GoogleBooks;


namespace SoftwareBookList.Services
{
    /// <summary>
    /// This class is responsible for mapping Google Book properties from an
    /// API response in the form of a JSON to our custom Book object for our
    /// web application.
    /// </summary>
    public class BookMappingService
    {
        public Book MapToBookEntity(GoogleBook googleBook)
        {
            var book = new Book
            {
                GoogleID = googleBook.Id,
                Title = googleBook.VolumeInfo.Title, // Can never be null in the db.
                Subtitle = googleBook.VolumeInfo?.Subtitle ?? string.Empty,
                Authors = googleBook.VolumeInfo?.Authors ?? new List<string>(),
                Description = googleBook.VolumeInfo?.Description ?? string.Empty,
                Publisher = googleBook.VolumeInfo?.Publisher ?? string.Empty,
                PublishedDate = googleBook.VolumeInfo?.PublishedDate ?? string.Empty,
                IndustryIdentifiers = googleBook.VolumeInfo?.IndustryIdentifiers ?? new List<IndustryIdentifier>(),
                SelfLink = googleBook.SelfLink ?? string.Empty,
                Categories = googleBook.VolumeInfo?.Categories ?? new List<string>(),
                SmallThumbnail = googleBook.VolumeInfo.ImageLinks.SmallThumbnail, // Can never be null in the db.
                Thumbnail = googleBook.VolumeInfo.ImageLinks.Thumbnail // Can never be null in the db.
            };

            return book;
        }
    }
}
