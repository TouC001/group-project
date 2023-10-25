using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1,
                column: "ThumbnailLink",
                value: "/lib/images/bluecar.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2,
                column: "ThumbnailLink",
                value: "/lib/images/robot.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 3,
                column: "ThumbnailLink",
                value: "/lib/images/legos.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1,
                column: "ThumbnailLink",
                value: "bluecar.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2,
                column: "ThumbnailLink",
                value: "robot.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 3,
                column: "ThumbnailLink",
                value: "legos.jpg");
        }
    }
}
