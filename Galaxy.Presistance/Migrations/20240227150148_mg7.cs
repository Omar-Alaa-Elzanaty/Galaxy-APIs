using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class mg7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                schema: "Galaxy",
                table: "ItemInStock",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ItemInStock_BarCode",
                schema: "Galaxy",
                table: "ItemInStock",
                column: "BarCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemInStock_BarCode",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.DropColumn(
                name: "IsInStock",
                schema: "Galaxy",
                table: "ItemInStock");
        }
    }
}
