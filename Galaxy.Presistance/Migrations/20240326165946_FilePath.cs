using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class FilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                schema: "Galaxy",
                table: "Suppliers",
                newName: "ImageFilePath");

            migrationBuilder.RenameColumn(
                name: "IdUrl",
                schema: "Galaxy",
                table: "Suppliers",
                newName: "IdFilePath");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                schema: "Galaxy",
                table: "Products",
                newName: "ImageFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFilePath",
                schema: "Galaxy",
                table: "Suppliers",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "IdFilePath",
                schema: "Galaxy",
                table: "Suppliers",
                newName: "IdUrl");

            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                schema: "Galaxy",
                table: "Products",
                newName: "ImageUrl");
        }
    }
}
