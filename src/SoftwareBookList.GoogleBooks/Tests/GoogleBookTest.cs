using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareBookList.GoogleBooks.Tests
{
    public class GoogleBookTest
    {
        /*
         * Add these lines at the end of Startup.cs to test.
         * 
         * var googleBookTest = new GoogleBookTest();
         * googleBookTest.CanCreateGoogleBook();
         * 
         */

        public GoogleBook CreateGoogleBook()
        {
            // Test creating a GoogleBook and saving it to the database
            var googleBook = new GoogleBook
            {
                Kind = "books#volume",
                Id = "Qt5DzQEACAAJ",
                Etag = "mHGz0MvC2po",
                SelfLink = "https://www.googleapis.com/books/v1/volumes/Qt5DzQEACAAJ",
                VolumeInfo = new VolumeInfo
                {
                    Title = "Composing Software",
                    Subtitle = "An Exploration of Functional Programming and Object Composition in JavaScript",
                    Authors = new List<string>
                    {
                        "Eric Elliott"
                    },
                    PublishedDate = "2018-12-27",
                    Description = "All software design is composition: the act of breaking complex problems down into smaller problems and composing those solutions. Most developers have a limited understanding of compositional techniques. It's time for that to change.In \"Composing Software\", Eric Elliott shares the fundamentals of composition, including both function composition and object composition, and explores them in the context of JavaScript. The book covers the foundations of both functional programming and object-oriented programming to help the reader better understand how to build and structure complex applications using simple building blocks.You'll learn: Functional programmingObject compositionHow to work with composite data structuresClosuresHigher order functionsFunctors (e.g., array.map)Monads (e.g., promises)TransducersLensesAll of this in the context of JavaScript, the most used programming language in the world. But the learning doesn't stop at JavaScript. You'll be able to apply these lessons to any language. This book is about the timeless principles of software composition and its lessons will outlast the hot languages and frameworks of today. Unlike most programming books, this one may still be relevant 20 years from now.This book began life as a popular blog post series that attracted hundreds of thousands of readers and influenced the way software is built at many high growth tech startups and Fortune 500 companies.",
                    IndustryIdentifiers = new List<IndustryIdentifier>
                    {
                        new IndustryIdentifier
                        {
                            Type = "ISBN_10",
                            Identifier = "1661212565"
                        },
                        new IndustryIdentifier
                        {
                            Type = "ISBN_13",
                            Identifier = "9781661212568"
                        }
                    },
                    ReadingModes = new ReadingModes
                    {
                        Text = false,
                        Image = false
                    },
                    PageCount = 246,
                    PrintType = "BOOK",
                    MaturityRating = "NOT_MATURE",
                    AllowAnonLogging = false,
                    ContentVersion = "preview-1.0.0",
                    PanelizationSummary = new PanelizationSummary
                    {
                        ContainsEpubBubbles = false,
                        ContainsImageBubbles = false
                    },
                    ImageLinks = new ImageLinks
                    {
                        SmallThumbnail = "http://books.google.com/books/content?id=Qt5DzQEACAAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api",
                        Thumbnail = "http://books.google.com/books/content?id=Qt5DzQEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api"
                    },
                    Language = "en",
                    PreviewLink = "http://books.google.com/books?id=Qt5DzQEACAAJ&dq=software&hl=&cd=1&source=gbs_api",
                    InfoLink = "http://books.google.com/books?id=Qt5DzQEACAAJ&dq=software&hl=&source=gbs_api",
                    CanonicalVolumeLink = "https://books.google.com/books/about/Composing_Software.html?hl=&id=Qt5DzQEACAAJ"
                },
                SaleInfo = new SaleInfo
                {
                    Country = "US",
                    Saleability = "NOT_FOR_SALE",
                    IsEbook = false
                },
                AccessInfo = new AccessInfo
                {
                    Country = "US",
                    Viewability = "NO_PAGES",
                    Embeddable = false,
                    PublicDomain = false,
                    TextToSpeechPermission = "ALLOWED",
                    Epub = new Epub
                    {
                        IsAvailable = false
                    },
                    Pdf = new Pdf
                    {
                        IsAvailable = false
                    },
                    WebReaderLink = "http://play.google.com/books/reader?id=Qt5DzQEACAAJ&hl=&source=gbs_api",
                    AccessViewStatus = "NONE",
                    QuoteSharingAllowed = false
                },
                SearchInfo = new SearchInfo
                {
                    TextSnippet = "But the learning doesn't stop at JavaScript. You'll be able to apply these lessons to any language. This book is about the timeless principles of software composition and its lessons will outlast the hot languages and frameworks of today."
                }
            };

            return googleBook;
        }

        public void CanCreateGoogleBook()
        {
            var googleBook = CreateGoogleBook();
        }

        public void CanDeleteGoogleBook()
        {
        }
    }
}