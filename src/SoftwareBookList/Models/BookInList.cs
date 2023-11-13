using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class BookInList
    {
        [Key, ForeignKey("Book")]
        public int BookID { get; set; }

        [Key, ForeignKey("BookListStatus")]
        public int StatusID { get; set; }

        [Key, ForeignKey("BookList")]
        public int BookListID { get; set; }

        public int RatingValue { get; set; }


        public Book Book { get; set; }
        public BookListStatus Status { get; set; }
        public BookList BookList { get; set; }



        public BookInList(int bookID, int statusID, int bookListID, int ratingValue)
        {
            BookID = bookID;
            StatusID = statusID;
            BookListID = bookListID;
            RatingValue = ratingValue;
        }

        public BookInList()
        {

        }
    }
}