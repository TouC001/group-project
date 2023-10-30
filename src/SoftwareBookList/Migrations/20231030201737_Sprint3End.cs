using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class Sprint3End : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Lists",
                keyColumn: "ListID",
                keyValue: 1);

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
                table: "Users",
                keyColumn: "UserID",
                keyValue: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookListStatus",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[] { 1, "The Bestest In The Wurld!" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "GoogleID", "SmallThumbnail", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { 1, "one", null, null, "Software Book 1" },
                    { 2, "two", null, null, "Software Book 2" },
                    { 3, "three", null, null, "Software Book 3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "EmailAddress", "FirstName", "IsAdmin", "LastName", "PasswordHash", "UserName" },
                values: new object[] { 50, "Tester@gmail.com", "Test", false, "Tester", "green", "" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "Bio", "Birthday", "ProfilePicture", "UserID", "UserName" },
                values: new object[] { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 50, "" });

            migrationBuilder.InsertData(
                table: "BookLists",
                columns: new[] { "BookListID", "BookID", "BookListStatusID", "ListID" },
                values: new object[,]
                {
                    { 1, 1, 1, 0 },
                    { 2, 2, 1, 0 },
                    { 3, 3, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "ListID", "Name", "UserID" },
                values: new object[] { 1, "Yolo", 50 });
        }
    }
}
