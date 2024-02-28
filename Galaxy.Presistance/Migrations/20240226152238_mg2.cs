using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                schema: "Account",
                table: "Users",
                newName: "Gander");

            migrationBuilder.AddColumn<int>(
                name: "TotalPay",
                schema: "Galaxy",
                table: "SuppliersInovices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                schema: "Galaxy",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                schema: "Galaxy",
                table: "ItemInStock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemInStock_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInStock_Suppliers_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock",
                column: "SupplierId",
                principalSchema: "Galaxy",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemInStock_Suppliers_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.DropIndex(
                name: "IX_ItemInStock_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.DropColumn(
                name: "TotalPay",
                schema: "Galaxy",
                table: "SuppliersInovices");

            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "Galaxy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.RenameColumn(
                name: "Gander",
                schema: "Account",
                table: "Users",
                newName: "Gender");
        }
    }
}
