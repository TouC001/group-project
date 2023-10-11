using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class AddUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Ratings_RateID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RateID",
                table: "Reviews",
                newName: "RatingID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RateID",
                table: "Reviews",
                newName: "IX_Reviews_RatingID");

            migrationBuilder.AlterColumn<double>(
                name: "RatingValue",
                table: "Ratings",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Ratings_RatingID",
                table: "Reviews",
                column: "RatingID",
                principalTable: "Ratings",
                principalColumn: "RatingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Ratings_RatingID",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "RatingID",
                table: "Reviews",
                newName: "RateID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RatingID",
                table: "Reviews",
                newName: "IX_Reviews_RateID");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "RatingValue",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Ratings_RateID",
                table: "Reviews",
                column: "RateID",
                principalTable: "Ratings",
                principalColumn: "RatingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
