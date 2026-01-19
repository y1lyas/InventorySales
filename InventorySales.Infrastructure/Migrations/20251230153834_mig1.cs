using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "KeycloakUserId",
                table: "Users",
                newName: "ExternalId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_KeycloakUserId",
                table: "Users",
                newName: "IX_Users_ExternalId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ExternalId",
                table: "Users",
                newName: "KeycloakUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                newName: "IX_Users_KeycloakUserId");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
