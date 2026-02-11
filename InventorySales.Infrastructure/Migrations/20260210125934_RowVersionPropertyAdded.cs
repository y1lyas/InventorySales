using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RowVersionPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Users",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "StockMovements",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Sales",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Products",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Products");
        }
    }
}
