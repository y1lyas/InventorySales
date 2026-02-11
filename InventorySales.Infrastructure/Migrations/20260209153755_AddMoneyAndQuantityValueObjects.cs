using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoneyAndQuantityValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Sales",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Sales",
                newName: "QuantityAmount");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Products",
                newName: "Price_Amount");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Sales",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Price_Currency",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Price_Currency",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Sales",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "QuantityAmount",
                table: "Sales",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                table: "Products",
                newName: "UnitPrice");
        }
    }
}
