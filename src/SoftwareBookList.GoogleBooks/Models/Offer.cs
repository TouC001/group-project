namespace SoftwareBookList.GoogleBooks
{
	public class Offer
    {
        public int FinskyOfferType { get; set; }
        public virtual ListPrice ListPrice { get; set; }
        public virtual RetailPrice RetailPrice { get; set; }
        public bool Giftable { get; set; }
    }
}
