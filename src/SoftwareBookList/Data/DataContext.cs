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
		public DbSet<List> Lists { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Rating> Ratings { get; set; }	
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserAccount> Accounts { get; set; }


		//public DbSet<BookViewModel> BookViewModel { get; set; }
		//public DbSet<GoogleBooksApiResponse> GoogleBooksApiResponse { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
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
				}

				);

			modelBuilder.Entity<BookList>()
				.HasKey(booklist => booklist.BookListID); // Specify the PK for BookList


			modelBuilder.Entity<BookList>()
				.HasOne(bl => bl.Book)
				.WithMany(b => b.BookLists);

			modelBuilder.Entity<BookList>()
				.HasOne(bl => bl.List)
				.WithMany(l => l.BooksInList);

			modelBuilder.Entity<BookList>()
				.HasOne(bl => bl.BookListStatus)
				.WithMany(bls => bls.BookLists);

			modelBuilder.Entity<BookList>().HasData(
				new BookList
				{
					BookListID = 1,
					BookListStatusID = 1,
					BookID = 1,
					LisID = 1

				},

				new BookList
				{
					BookListID = 2,
					BookListStatusID = 1,
					BookID = 2,
					LisID = 1

				},

				new BookList
				{
					BookListID = 3,
					BookListStatusID = 1,
					BookID = 3,
					LisID = 1

				}

				);



			modelBuilder.Entity<BookListStatus>()
				.HasKey(status => status.StatusID); // Specify the PK for BookTag

			modelBuilder.Entity<BookListStatus>().HasData(
				new BookListStatus
				{
					StatusID = 1,
					StatusName = "The Bestest In The Wurld!"
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




			modelBuilder.Entity<List>()
				.HasKey(list => list.ListID); // Specify the PK for List

			modelBuilder.Entity<List>().HasData(
				new List
				{
					ListID = 1, UserID = 50, Name = "Yolo"
				}

				);


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
