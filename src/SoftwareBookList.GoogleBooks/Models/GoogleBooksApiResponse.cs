using System;
using System.Collections.Generic;

namespace SoftwareBookList.GoogleBooks
{
	public class GoogleBooksAPIResponse
	{
		public string Kind { get; set; }
		public int TotalItems { get; set; }
		public virtual List<GoogleBook> Items { get; set; }
	}
}
