using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookListStatus",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[] { 1, "The Bestest In The Wurld!" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Authors", "Description", "GoogleID", "ISBN", "PublishedDate", "ThumbnailLink", "Title" },
                values: new object[,]
                {
                    { 1, "Chang", "Guess what? Chicken Butt.", "one", "abc", "10/10/2019", "bluecar.jpg", "Software Book 1" },
                    { 2, "Matthew", "Learn how to make space ships go vroom vroom.", "two", "cba", "10/10/2010", "robot.jpg", "Software Book 2" },
                    { 3, "Dillon", "I am so much better than all of you combined.", "three", "pol", "10/10/2023", "legos.jpg", "Software Book 3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "EmailAddress", "FirstName", "LastName", "PasswordHash", "UserName" },
                values: new object[] { 50, "Tester@gmail.com", "Test", "Tester", "green", "" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "Bio", "Birthday", "ProfilePicture", "UserID", "UserName" },
                values: new object[] { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 50, "" });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "ListID", "Name", "UserID" },
                values: new object[] { 1, "Yolo", 50 });

            migrationBuilder.InsertData(
                table: "BookLists",
                columns: new[] { "BookListID", "BookID", "BookListStatusID", "LisID" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookListStatus",
                keyColumn: "StatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lists",
                keyColumn: "ListID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 50);
        }
    }
}
