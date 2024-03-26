using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class FileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFilePath",
                schema: "Galaxy",
                table: "Suppliers",
                newName: "ImageFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                schema: "Galaxy",
                table: "Suppliers",
                newName: "ImageFilePath");
        }
    }
}
