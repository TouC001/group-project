using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Models;

namespace SoftwareBookList.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) 
        { }  
        
        public DbSet<Book> Books { get; set; }
    }
}
