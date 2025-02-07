using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTbleImgesEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ProductImages",
                newName: "ImagePathTheard");

            migrationBuilder.AddColumn<string>(
                name: "ImagePathFirst",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePathFord",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePathSeconed",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePathFirst",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ImagePathFord",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ImagePathSeconed",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "ImagePathTheard",
                table: "ProductImages",
                newName: "ImagePath");
        }
    }
}
