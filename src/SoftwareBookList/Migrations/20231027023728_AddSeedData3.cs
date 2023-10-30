using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_BookListStatus_BookListStatusID",
                table: "BookLists");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_Books_BookID",
                table: "BookLists");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_Lists_LisID",
                table: "BookLists");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_BookLists_BookID",
                table: "BookLists");

            migrationBuilder.DropIndex(
                name: "IX_BookLists_BookListStatusID",
                table: "BookLists");

            migrationBuilder.DropIndex(
                name: "IX_BookLists_LisID",
                table: "BookLists");

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "BookLists");

            migrationBuilder.DropColumn(
                name: "BookListStatusID",
                table: "BookLists");

            migrationBuilder.RenameColumn(
                name: "LisID",
                table: "BookLists",
                newName: "UserID");

            migrationBuilder.CreateTable(
                name: "BookInLists",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    BookListID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInLists", x => new { x.BookID, x.StatusID, x.BookListID });
                    table.ForeignKey(
                        name: "FK_BookInLists_BookListStatus_StatusID",
                        column: x => x.StatusID,
                        principalTable: "BookListStatus",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInLists_BookLists_BookListID",
                        column: x => x.BookListID,
                        principalTable: "BookLists",
                        principalColumn: "BookListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInLists_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookInLists",
                columns: new[] { "BookID", "BookListID", "StatusID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "BookListStatus",
                keyColumn: "StatusID",
                keyValue: 1,
                column: "StatusName",
                value: "Read");

            migrationBuilder.InsertData(
                table: "BookListStatus",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[,]
                {
                    { 2, "Plan to Read" },
                    { 3, "Currently Reading" }
                });

            migrationBuilder.UpdateData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 1,
                column: "UserID",
                value: 50);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Authors", "Description", "GoogleID", "ISBN", "PublishedDate", "ThumbnailLink", "Title" },
                values: new object[,]
                {
                    { 4, "Tou", "I am so much better than all of you combined.", "four", "lll", "10/10/2020", "ducks.jpg", "Software Book 4" },
                    { 5, "Kennen", "I am so much better than all of you combined.", "five", "ppp", "10/10/1997", "teddy.jpg", "Software Book 5" },
                    { 6, "Kyle", "I am so much better than all of you combined.", "six", "uu", "10/10/2010", "bluecar.jpg", "Software Book 6" }
                });

            migrationBuilder.InsertData(
                table: "BookInLists",
                columns: new[] { "BookID", "BookListID", "StatusID" },
                values: new object[,]
                {
                    { 4, 1, 2 },
                    { 5, 1, 2 },
                    { 6, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_UserID",
                table: "BookLists",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookInLists_BookListID",
                table: "BookInLists",
                column: "BookListID");

            migrationBuilder.CreateIndex(
                name: "IX_BookInLists_StatusID",
                table: "BookInLists",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_Users_UserID",
                table: "BookLists",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_Users_UserID",
                table: "BookLists");

            migrationBuilder.DropTable(
                name: "BookInLists");

            migrationBuilder.DropIndex(
                name: "IX_BookLists_UserID",
                table: "BookLists");

            migrationBuilder.DeleteData(
                table: "BookListStatus",
                keyColumn: "StatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookListStatus",
                keyColumn: "StatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "BookLists",
                newName: "LisID");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "BookLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookListStatusID",
                table: "BookLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    ListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.ListID);
                    table.ForeignKey(
                        name: "FK_Lists_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "BookListStatus",
                keyColumn: "StatusID",
                keyValue: 1,
                column: "StatusName",
                value: "The Bestest In The Wurld!");

            migrationBuilder.UpdateData(
                table: "BookLists",
                keyColumn: "BookListID",
                keyValue: 1,
                columns: new[] { "BookID", "BookListStatusID", "LisID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "ListID", "Name", "UserID" },
                values: new object[] { 1, "Yolo", 50 });

            migrationBuilder.InsertData(
                table: "BookLists",
                columns: new[] { "BookListID", "BookID", "BookListStatusID", "LisID" },
                values: new object[,]
                {
                    { 2, 2, 1, 1 },
                    { 3, 3, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_BookID",
                table: "BookLists",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_BookListStatusID",
                table: "BookLists",
                column: "BookListStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_LisID",
                table: "BookLists",
                column: "LisID");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UserID",
                table: "Lists",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_BookListStatus_BookListStatusID",
                table: "BookLists",
                column: "BookListStatusID",
                principalTable: "BookListStatus",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_Books_BookID",
                table: "BookLists",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_Lists_LisID",
                table: "BookLists",
                column: "LisID",
                principalTable: "Lists",
                principalColumn: "ListID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
