﻿namespace SoftwareBookList.Models
{
    public class Book
    {
        public string GoogleID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string PublishedDate { get; set; }
        public string ThumbnailLink { get; set; }
    }
}
