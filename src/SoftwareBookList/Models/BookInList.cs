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

<<<<<<< HEAD
        [Key, ForeignKey("Rating")]
        public double RatingValue { get; set; }
=======
        public int RatingValue { get; set; }

>>>>>>> 0e943a910c5a3567b2127741fcaffb98242a5d37

        public Book Book { get; set; }
        public BookListStatus Status { get; set; }
        public BookList BookList { get; set; }


<<<<<<< HEAD
        public BookInList(int bookID, int statusID, int bookListID, double ratingValue)
=======

        public BookInList(int bookID, int statusID, int bookListID, int ratingValue)
>>>>>>> 0e943a910c5a3567b2127741fcaffb98242a5d37
        {
            BookID = bookID;
            StatusID = statusID;
            BookListID = bookListID;
            RatingValue = ratingValue;
<<<<<<< HEAD
=======
        }

        public BookInList()
        {

>>>>>>> 0e943a910c5a3567b2127741fcaffb98242a5d37
        }
    }
}