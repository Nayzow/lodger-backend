using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LodgerBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_settings_user_id",
                table: "settings");

            migrationBuilder.CreateIndex(
                name: "IX_settings_user_id",
                table: "settings",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_settings_user_id",
                table: "settings");

            migrationBuilder.CreateIndex(
                name: "IX_settings_user_id",
                table: "settings",
                column: "user_id",
                unique: true);
        }
    }
}
