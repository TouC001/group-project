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
                Title = googleBook.VolumeInfo.Title,
                Subtitle = googleBook.VolumeInfo.Subtitle,
                Authors = googleBook.VolumeInfo.Authors,
                Description = googleBook.VolumeInfo.Description,
                Publisher = googleBook.VolumeInfo.Publisher,
                PublishedDate = googleBook.VolumeInfo.PublishedDate,
                IndustryIdentifiers = googleBook.VolumeInfo.IndustryIdentifiers,
                SelfLink = googleBook.SelfLink,
                Categories = googleBook.VolumeInfo.Categories,
                SmallThumbnail = googleBook.VolumeInfo.ImageLinks.SmallThumbnail,
                Thumbnail = googleBook.VolumeInfo.ImageLinks.Thumbnail
            };

            return book;
        }
    }
}
