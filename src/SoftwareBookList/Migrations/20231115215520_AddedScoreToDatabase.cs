using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareBookList.Migrations
{
    /// <inheritdoc />
    public partial class AddedScoreToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DbTotalScore",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbTotalScore",
                table: "Books");
        }
    }
}
