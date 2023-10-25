using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareBookList.Models
{
    public class BookList
    {
        [Key]
        public int BookListID { get; set; }

        [ForeignKey("BookListStatus")]
        public int BookListStatusID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }

        [ForeignKey("List")]
        public int ListID { get; set; }


        // Navigation property
        public BookListStatus BookListStatus { get; set; }



        // Navigation property for the associated book
        public Book Book { get; set; }



        // Navigation property for the associated list
        public List List { get; set; }
    }
}