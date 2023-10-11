using System;
using System.Collections.Generic;

namespace SoftwareBookList.GoogleBooks
{
	public class GoogleBooksApiResponse
    {
        public string Kind { get; set; }
        public int TotalItems { get; set; }
        public List<GoogleBook> Items { get; set; }
    }
}
