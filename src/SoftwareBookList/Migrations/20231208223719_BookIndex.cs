using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class BookIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_BookID_DbTotalScore",
                table: "Books",
                columns: new[] { "BookID", "DbTotalScore" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_BookID_DbTotalScore",
                table: "Books");
        }
    }
}
