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
		public DbSet<BookList> BookLists { get; set; }
		public DbSet<BookListStatus> BookListStatus { get; set; }
		public DbSet<BookTag> BookTags { get; set; }
		public DbSet<Discussion> Discussions { get; set; }
		public DbSet<List> Lists { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }


		//public DbSet<BookViewModel> BookViewModel { get; set; }
		//public DbSet<GoogleBooksApiResponse> GoogleBooksApiResponse { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasKey(book => book.BookID); // Specify the PK for Book

			modelBuilder.Entity<BookList>()
				.HasKey(booklist => booklist.BookListID); // Specify the PK for BookList

			modelBuilder.Entity<BookListStatus>()
				.HasKey(status => status.StatusID); // Specify the PK for BookTag

			modelBuilder.Entity<BookTag>()
				.HasKey(booktag => booktag.BookTagID); // Specify the PK for BookTag




			modelBuilder.Entity<Discussion>()
				.HasKey(discussion => discussion.DiscussionID); // Specify the PK for Discussion

			modelBuilder.Entity<Discussion>()
				.HasOne(discussion => discussion.Creator) // A Discussion is associated with one User (the creator)
				.WithMany() // A User can be associated with many Discussions
				.HasForeignKey(discussion => discussion.UserID) // The foreign key in Discussion is UserID
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

			modelBuilder.Entity<Discussion>()
				.HasOne(discussion => discussion.User) // Configure the relationship for the 'User' navigation property in 'Discussion'.
				.WithMany(user => user.Discussions) // Indicates that a 'User' can have many discussions.
				.HasForeignKey(discussion => discussion.UserID) // Specifies that the foreign key in the 'Discussion' table is 'UserID'.
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for this relationship.




			modelBuilder.Entity<List>()
				.HasKey(list => list.ListID); // Specify the PK for List




			modelBuilder.Entity<Message>()
				.HasKey(message => message.MessageID); // Specify the PK for Message

			modelBuilder.Entity<Message>()
				.HasOne(message => message.Sender) // A Message is sent by one User (the sender)
				.WithMany(user => user.SentMessages) // A User can send many Messages
				.HasForeignKey(message => message.SenderID) // The foreign key in Message is SenderID
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

			modelBuilder.Entity<Message>()
				.HasOne(message => message.Recipient) // A Message is received by one User (the recipient)
				.WithMany(user => user.ReceivedMessages) // A User can receive many Messages
				.HasForeignKey(message => message.RecipientID) // The foreign key in Message is RecipientID
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete




			modelBuilder.Entity<Rating>()
				.HasKey(rating => rating.RatingID); // Specify the PK for BookTag

			modelBuilder.Entity<Review>()
				.HasKey(review => review.ReviewID); // Specify the PK for Review

			modelBuilder.Entity<Tag>()
				.HasKey(tag => tag.TagId); // Specify the PK for BookTag

			modelBuilder.Entity<User>()
				.HasKey(user => user.UserID); // Specify the PK for User

			base.OnModelCreating(modelBuilder);
		}
	}
}
