namespace SoftwareBookList.GoogleBooks
{
    public class GoogleBook
    {
        public string Id { get; set; } // Primary key

        // Other properties
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string SelfLink { get; set; }

        // Complex types (EF Core will handle the relationships)
        public virtual VolumeInfo VolumeInfo { get; set; }
        public virtual SaleInfo SaleInfo { get; set; }
        public virtual AccessInfo AccessInfo { get; set; }
        public virtual SearchInfo SearchInfo { get; set; }
    }
}
