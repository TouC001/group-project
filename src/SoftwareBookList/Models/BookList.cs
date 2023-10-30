using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class BookList
    {
        [Key]
        public int BookListID { get; set; }


        [ForeignKey("User")]
        public int UserID { get; set; }


        // Navigation property
        public User User { get; set; }

        public List<BookInList> BookInLists { get; set; }

    }
}