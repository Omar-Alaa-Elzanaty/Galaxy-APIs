using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class updateNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemInStock_Products_ProductId",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInStock_Suppliers_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemInStock",
                schema: "Galaxy",
                table: "ItemInStock");

            migrationBuilder.RenameTable(
                name: "ItemInStock",
                schema: "Galaxy",
                newName: "ItemsInStock",
                newSchema: "Galaxy");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInStock_SupplierId",
                schema: "Galaxy",
                table: "ItemsInStock",
                newName: "IX_ItemsInStock_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInStock_ProductId",
                schema: "Galaxy",
                table: "ItemsInStock",
                newName: "IX_ItemsInStock_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInStock_BarCode",
                schema: "Galaxy",
                table: "ItemsInStock",
                newName: "IX_ItemsInStock_BarCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsInStock",
                schema: "Galaxy",
                table: "ItemsInStock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsInStock_Products_ProductId",
                schema: "Galaxy",
                table: "ItemsInStock",
                column: "ProductId",
                principalSchema: "Galaxy",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsInStock_Suppliers_SupplierId",
                schema: "Galaxy",
                table: "ItemsInStock",
                column: "SupplierId",
                principalSchema: "Galaxy",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsInStock_Products_ProductId",
                schema: "Galaxy",
                table: "ItemsInStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsInStock_Suppliers_SupplierId",
                schema: "Galaxy",
                table: "ItemsInStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsInStock",
                schema: "Galaxy",
                table: "ItemsInStock");

            migrationBuilder.RenameTable(
                name: "ItemsInStock",
                schema: "Galaxy",
                newName: "ItemInStock",
                newSchema: "Galaxy");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsInStock_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock",
                newName: "IX_ItemInStock_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsInStock_ProductId",
                schema: "Galaxy",
                table: "ItemInStock",
                newName: "IX_ItemInStock_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsInStock_BarCode",
                schema: "Galaxy",
                table: "ItemInStock",
                newName: "IX_ItemInStock_BarCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemInStock",
                schema: "Galaxy",
                table: "ItemInStock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInStock_Products_ProductId",
                schema: "Galaxy",
                table: "ItemInStock",
                column: "ProductId",
                principalSchema: "Galaxy",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInStock_Suppliers_SupplierId",
                schema: "Galaxy",
                table: "ItemInStock",
                column: "SupplierId",
                principalSchema: "Galaxy",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
