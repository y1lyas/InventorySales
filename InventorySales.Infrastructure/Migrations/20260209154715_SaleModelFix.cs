using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SaleModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Sales",
                newName: "TotalPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Sales",
                newName: "TotalAmount");
        }
    }
}
