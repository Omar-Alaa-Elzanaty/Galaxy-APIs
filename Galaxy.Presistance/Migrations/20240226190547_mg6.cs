using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class mg6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CusotmersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                newName: "Quantity");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CusotmersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices",
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
                name: "FK_CusotmersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CusotmersInvoices_Customers_CustomerId",
                schema: "Galaxy",
                table: "CusotmersInvoices",
                column: "CustomerId",
                principalSchema: "Galaxy",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
