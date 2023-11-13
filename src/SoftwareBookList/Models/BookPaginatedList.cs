namespace SoftwareBookList.Models
{
    public class BookPaginatedList<T>
    {
        public IQueryable<T> Books { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public BookPaginatedList(IQueryable<T> books, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(books.Count() / (double)pageSize);
            Books = books.Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public bool HasPreviousPage
        {
            get { return CurrentPage > 1; }
        }

        public bool HasNextPage
        {
            get { return CurrentPage < TotalPages; }
        }

        public int PageIndex
        {
            get { return CurrentPage; }
        }
    }
}
