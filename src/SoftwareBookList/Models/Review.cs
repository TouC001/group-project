using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }

        [ForeignKey("Rate")]
        public int RateID { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ReviewText { get; set; }
    }
}
