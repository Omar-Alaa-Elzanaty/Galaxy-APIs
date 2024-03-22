using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CusotmersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInvoiceItem_CusotmersInvoices_CustomerInvoiceId",
                schema: "Galaxy",
                table: "CustomerInvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CusotmersInvoices",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.RenameTable(
                name: "CusotmersInvoices",
                schema: "Galaxy",
                newName: "CustomersInvoices",
                newSchema: "Galaxy");

            migrationBuilder.RenameIndex(
                name: "IX_CusotmersInvoices_CustomerId",
                schema: "Galaxy",
                table: "CustomersInvoices",
                newName: "IX_CustomersInvoices_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomersInvoices",
                schema: "Galaxy",
                table: "CustomersInvoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInvoiceItem_CustomersInvoices_CustomerInvoiceId",
                schema: "Galaxy",
                table: "CustomerInvoiceItem",
                column: "CustomerInvoiceId",
                principalSchema: "Galaxy",
                principalTable: "CustomersInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CustomersInvoices",
                column: "CustomerId",
                principalSchema: "Galaxy",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInvoiceItem_CustomersInvoices_CustomerInvoiceId",
                schema: "Galaxy",
                table: "CustomerInvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CustomersInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomersInvoices",
                schema: "Galaxy",
                table: "CustomersInvoices");

            migrationBuilder.RenameTable(
                name: "CustomersInvoices",
                schema: "Galaxy",
                newName: "CusotmersInvoices",
                newSchema: "Galaxy");

            migrationBuilder.RenameIndex(
                name: "IX_CustomersInvoices_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                newName: "IX_CusotmersInvoices_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CusotmersInvoices",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CusotmersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                column: "CustomerId",
                principalSchema: "Galaxy",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInvoiceItem_CusotmersInvoices_CustomerInvoiceId",
                schema: "Galaxy",
                table: "CustomerInvoiceItem",
                column: "CustomerInvoiceId",
                principalSchema: "Galaxy",
                principalTable: "CusotmersInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
