using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class BookTag
    {

        [Key]
        public int BookTagID { get; set; }

        [ForeignKey("Book")]
        [Column(Order = 1)]
        public int BookID { get; set; }

        [ForeignKey("Tag")]
        [Column(Order = 2)]
        public int TagID { get; set; }



        // Navigation property for the book in the many-to-many relationship
        public Book Book { get; set; }



        // Navigation property for the tag in the many-to-many relationship
        public Tag Tag { get; set; }
    }
}
