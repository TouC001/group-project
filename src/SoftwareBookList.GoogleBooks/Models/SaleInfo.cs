namespace SoftwareBookList.GoogleBooks
{
	public class SaleInfo
    {
        public string Country { get; set; }
        public string Saleability { get; set; }
        public bool IsEbook { get; set; }
        public virtual ListPrice ListPrice { get; set; }
        public virtual RetailPrice RetailPrice { get; set; }
        public string BuyLink { get; set; }
        public virtual List<Offer> Offers { get; set; }
    }
}
