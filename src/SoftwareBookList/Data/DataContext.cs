using Microsoft.EntityFrameworkCore;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace SoftwareBookList.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options) 
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<BookList> BookLists { get; set; }
		public DbSet<BookListStatus> BookListStatus { get; set; }
		public DbSet<BookTag> BookTags { get; set; }
		public DbSet<Discussion> Discussions { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserAccount> Accounts { get; set; }
		public DbSet<BookInList> BookInLists { get; set; }
		public DbSet<Comment> comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<BookInList>()
				.HasKey(bil => new { bil.BookID, bil.StatusID, bil.BookListID });

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

			modelBuilder.Entity<Comment>()
				.HasKey(comment => comment.CommentID);

			modelBuilder.Entity<Comment>()
				.HasOne(comment => comment.Commentor) // Configure the relationship for the 'User' navigation property in 'Comment'.
				.WithMany(user => user.UserComment) // Indicates that a 'User' can have many Comments.
				.HasForeignKey(comment => comment.UserID) // Specifies that the foreign key in the 'Comment' table is 'UserID'.
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for this relationship.

			modelBuilder.Entity<Book>()
				.HasKey(book => book.BookID); // Specify the PK for Book

			modelBuilder.Entity<Book>()
				.HasMany(book => book.Comments) // A Book can have many Comments
				.WithOne(comment => comment.CommentedBook) // A Comment is associated with one Book
				.HasForeignKey(comment => comment.BookID) // The foreign key in Comment is BookID
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

			modelBuilder.Entity<BookList>()
				.HasKey(booklist => booklist.BookListID); // Specify the PK for BookList

			// Define a one-to-one relationship between User and BookList
			modelBuilder.Entity<User>()
				.HasOne(user => user.BookList)
				.WithOne(bookList => bookList.User)
				.HasForeignKey<BookList>(bookList => bookList.UserID);

			modelBuilder.Entity<User>()
				.HasMany(user => user.UserComment) // A User can have many Comments
				.WithOne(comment => comment.Commentor) // A Comment is associated with one User
				.HasForeignKey(comment => comment.UserID)
				.OnDelete(DeleteBehavior.Restrict);

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


			modelBuilder.Entity<Review>()
				.HasKey(review => review.ReviewID); // Specify the PK for Review


			modelBuilder.Entity<Review>()
				.HasOne(u => u.User)
				.WithMany(u => u.ReviewsGiven)
				.HasForeignKey(u => u.UserID);

			modelBuilder.Entity<Review>()
				.HasOne(b => b.Book)
				.WithMany(b => b.Reviews)
				.HasForeignKey(u => u.BookID);


			modelBuilder.Entity<Tag>()
				.HasKey(tag => tag.TagId); // Specify the PK for BookTag

			modelBuilder.Entity<User>()
				.HasKey(user => user.UserID); // Specify the PK for User

			modelBuilder.Entity<User>()
				.HasMany(u => u.UserAccounts)
				.WithOne(ua => ua.User)
				.HasForeignKey(ua => ua.UserID);


			modelBuilder.Entity<UserAccount>()
				.HasKey(account => account.AccountID); // Specify the PK for Account


			base.OnModelCreating(modelBuilder);

		}

		public async Task RefreshBookRating(int BookID)
		{
			Book? book = await this.Books.AsNoTracking().Include(b => b.BookInLists).Include(b => b.Reviews).FirstOrDefaultAsync(b => b.BookID == BookID);

			if (book != null)
			{
				book.DbTotalScore = book.TotalScore();
				book.Reviews = null;
				book.BookInLists = null;

				this.Books.Update(book);
				await this.SaveChangesAsync();
			}
		}

		public int GetBookIDByGoogleID(string googleID)
		{
			Book? book = this.Books.FirstOrDefault(google => google.GoogleID == googleID);

			return book?.BookID ?? 0;
		}

		public List<Comment> GetCommentForBooks(string googleID)
		{
			int bookID = GetBookIDByGoogleID(googleID);

			return this.comments
				.Include(Comment => Comment.Commentor)
				.Where(comment => comment.BookID == bookID).ToList();
		}

		public User GetUserNameFromId(int userId)
		{
			return this.Users.Find(userId);
		}
	}
}
