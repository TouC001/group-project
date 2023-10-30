﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftwareBookList.Data;

#nullable disable

namespace SoftwareBookList.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SoftwareBookList.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<string>("GoogleID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmallThumbnail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("SoftwareBookList.Models.BookList", b =>
                {
                    b.Property<int>("BookListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookListID"));

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("BookListStatusID")
                        .HasColumnType("int");

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.HasKey("BookListID");

                    b.HasIndex("BookID");

                    b.HasIndex("BookListStatusID");

                    b.HasIndex("ListID");

                    b.ToTable("BookLists");
                });

            modelBuilder.Entity("SoftwareBookList.Models.BookListStatus", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusID"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusID");

                    b.ToTable("BookListStatus");
                });

            modelBuilder.Entity("SoftwareBookList.Models.BookTag", b =>
                {
                    b.Property<int>("BookTagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookTagID"));

                    b.Property<int>("BookID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("TagID")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("BookTagID");

                    b.HasIndex("BookID");

                    b.HasIndex("TagID");

                    b.ToTable("BookTags");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Discussion", b =>
                {
                    b.Property<int>("DiscussionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscussionID"));

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorUserID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DiscussionID");

                    b.HasIndex("BookID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("UserID");

                    b.ToTable("Discussions");
                });

            modelBuilder.Entity("SoftwareBookList.Models.List", b =>
                {
                    b.Property<int>("ListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ListID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ListID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("DiscussionID")
                        .HasColumnType("int");

                    b.Property<int>("RecipientID")
                        .HasColumnType("int");

                    b.Property<int>("SenderID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("MessageID");

                    b.HasIndex("DiscussionID");

                    b.HasIndex("RecipientID");

                    b.HasIndex("SenderID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Rating", b =>
                {
                    b.Property<int>("RatingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingID"));

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.HasKey("RatingID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("RateID")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.HasIndex("BookID");

                    b.HasIndex("RateID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SoftwareBookList.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SoftwareBookList.Models.BookList", b =>
                {
                    b.HasOne("SoftwareBookList.Models.Book", "Book")
                        .WithMany("BookLists")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.BookListStatus", "BookListStatus")
                        .WithMany("BookLists")
                        .HasForeignKey("BookListStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.List", "List")
                        .WithMany("BooksInList")
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("BookListStatus");

                    b.Navigation("List");
                });

            modelBuilder.Entity("SoftwareBookList.Models.BookTag", b =>
                {
                    b.HasOne("SoftwareBookList.Models.Book", "Book")
                        .WithMany("BookTags")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.Tag", "Tag")
                        .WithMany("BookTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Discussion", b =>
                {
                    b.HasOne("SoftwareBookList.Models.Book", "AssociatedBook")
                        .WithMany("Discussions")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.User", "User")
                        .WithMany("Discussions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssociatedBook");

                    b.Navigation("Creator");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoftwareBookList.Models.List", b =>
                {
                    b.HasOne("SoftwareBookList.Models.User", "User")
                        .WithOne("List")
                        .HasForeignKey("SoftwareBookList.Models.List", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Message", b =>
                {
                    b.HasOne("SoftwareBookList.Models.Discussion", "Discussion")
                        .WithMany("Messages")
                        .HasForeignKey("DiscussionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.User", "Recipient")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("RecipientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Discussion");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Review", b =>
                {
                    b.HasOne("SoftwareBookList.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.Rating", "Rating")
                        .WithMany("Reviews")
                        .HasForeignKey("RateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareBookList.Models.User", "User")
                        .WithMany("ReviewsGiven")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Rating");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Book", b =>
                {
                    b.Navigation("BookLists");

                    b.Navigation("BookTags");

                    b.Navigation("Discussions");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("SoftwareBookList.Models.BookListStatus", b =>
                {
                    b.Navigation("BookLists");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Discussion", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("SoftwareBookList.Models.List", b =>
                {
                    b.Navigation("BooksInList");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Rating", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("SoftwareBookList.Models.Tag", b =>
                {
                    b.Navigation("BookTags");
                });

            modelBuilder.Entity("SoftwareBookList.Models.User", b =>
                {
                    b.Navigation("Discussions");

                    b.Navigation("List")
                        .IsRequired();

                    b.Navigation("ReceivedMessages");

                    b.Navigation("ReviewsGiven");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
