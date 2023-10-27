using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Models;
using System.Collections.Generic;
using System.Net;

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
		public DbSet<Message> Messages { get; set; }
		public DbSet<Rating> Ratings { get; set; }	
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserAccount> Accounts { get; set; }
		public DbSet<BookInList> BookInLists { get; set; }


		//public DbSet<BookViewModel> BookViewModel { get; set; }
		//public DbSet<GoogleBooksApiResponse> GoogleBooksApiResponse { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// Relationship between BookInList and Book
			modelBuilder.Entity<BookInList>()
				.HasOne(bil => bil.Book)
				.WithMany(book => book.BookInLists)
				.HasForeignKey(bil => bil.BookID);

			// Relationship between BookInList and BookListStatus
			modelBuilder.Entity<BookInList>()
				.HasOne(bil => bil.Status)
				.WithMany(status => status.BookInList)
				.HasForeignKey(bil => bil.StatusID);

			// Relationship between BookInList and BookList
			modelBuilder.Entity<BookInList>()
				.HasOne(bil => bil.BookList)
				.WithMany(list => list.BookInLists)
				.HasForeignKey(bil => bil.BookListID);

			modelBuilder.Entity<BookInList>().HasData(
				new BookInList(1, 1, 1),
				new BookInList(2, 1, 1),
				new BookInList(3, 1, 1),
				new BookInList(4, 2, 1),
				new BookInList(5, 2, 1),
				new BookInList(6, 3, 1)

				);;



			modelBuilder.Entity<Book>()
				.HasKey(book => book.BookID); // Specify the PK for Book

			modelBuilder.Entity<Book>().HasData(
				new Book
				{
					BookID = 1, 
					GoogleID = "one", 
					Title = "Software Book 1", 
					Authors = "Chang", 
					Description = "Guess what? Chicken Butt.", 
					ISBN = "abc", 
					PublishedDate = "10/10/2019", 
					ThumbnailLink = "bluecar.jpg"
				},

				new Book
				{
					BookID = 2,
					GoogleID = "two",
					Title = "Software Book 2",
					Authors = "Matthew",
					Description = "Learn how to make space ships go vroom vroom.",
					ISBN = "cba",
					PublishedDate = "10/10/2010",
					ThumbnailLink = "robot.jpg"
				},

				new Book
				{
					BookID = 3,
					GoogleID = "three",
					Title = "Software Book 3",
					Authors = "Dillon",
					Description = "I am so much better than all of you combined.",
					ISBN = "pol",
					PublishedDate = "10/10/2023",
					ThumbnailLink = "legos.jpg"
				},

				new Book
				{
					BookID = 4,
					GoogleID = "four",
					Title = "Software Book 4",
					Authors = "Tou",
					Description = "I am so much better than all of you combined.",
					ISBN = "lll",
					PublishedDate = "10/10/2020",
					ThumbnailLink = "ducks.jpg"
				},

				new Book
				{
					BookID = 5,
					GoogleID = "five",
					Title = "Software Book 5",
					Authors = "Kennen",
					Description = "I am so much better than all of you combined.",
					ISBN = "ppp",
					PublishedDate = "10/10/1997",
					ThumbnailLink = "teddy.jpg"
				},

				new Book
				{
					BookID = 6,
					GoogleID = "six",
					Title = "Software Book 6",
					Authors = "Kyle",
					Description = "I am so much better than all of you combined.",
					ISBN = "uu",
					PublishedDate = "10/10/2010",
					ThumbnailLink = "bluecar.jpg"
				}

				);

			modelBuilder.Entity<BookList>()
				.HasKey(booklist => booklist.BookListID); // Specify the PK for BookList

			// Define a one-to-one relationship between User and BookList
			modelBuilder.Entity<User>()
				.HasOne(user => user.BookList)
				.WithOne(bookList => bookList.User)
				.HasForeignKey<BookList>(bookList => bookList.UserID);


			modelBuilder.Entity<BookList>().HasData(
				new BookList
				{
					BookListID = 1,
					UserID = 1

				}

				);



			modelBuilder.Entity<BookListStatus>()
				.HasKey(status => status.StatusID); // Specify the PK for BookTag

			modelBuilder.Entity<BookListStatus>().HasData(
				new BookListStatus
				{
					StatusID = 1,
					StatusName = "Read"
				},
				new BookListStatus
				{
					StatusID = 2,
					StatusName = "Plan to Read"
				},
				new BookListStatus
				{
					StatusID = 3,
					StatusName = "Currently Reading"
				}
				);


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

			modelBuilder.Entity<User>()
				.HasMany(u => u.UserAccounts)
				.WithOne(ua => ua.User)
				.HasForeignKey(ua => ua.UserID);

			modelBuilder.Entity<User>().HasData(
				new User
				{
					UserID = 50,
					FirstName = "Test",
					LastName = "Tester",
					EmailAddress = "Tester@gmail.com",
					PasswordHash = "green",
					UserName = "",
				}

				);


			modelBuilder.Entity<UserAccount>()
				.HasKey(account => account.AccountID); // Specify the PK for Account

			modelBuilder.Entity<UserAccount>().HasData(
				new UserAccount
				{
					AccountID = 1,
					UserID = 50,
					UserName = "",
					ProfilePicture = "",
					Bio = "",
					Birthday = DateTime.MinValue,
				}

				);


			base.OnModelCreating(modelBuilder);
		}
	}
}
