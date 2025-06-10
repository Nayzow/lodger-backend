using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LodgerBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUserAddRentalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rental_files_users_user_id",
                table: "rental_files");

            migrationBuilder.DropForeignKey(
                name: "FK_users_rental_files_rental_file_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_rental_file_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_rental_files_user_id",
                table: "rental_files");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "rental_files");

            migrationBuilder.AlterColumn<int>(
                name: "rental_file_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_rental_file_id",
                table: "users",
                column: "rental_file_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rental_files_rental_file_id",
                table: "users",
                column: "rental_file_id",
                principalTable: "rental_files",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_rental_files_rental_file_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_rental_file_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "rental_file_id",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "rental_files",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_rental_file_id",
                table: "users",
                column: "rental_file_id");

            migrationBuilder.CreateIndex(
                name: "IX_rental_files_user_id",
                table: "rental_files",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_rental_files_users_user_id",
                table: "rental_files",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rental_files_rental_file_id",
                table: "users",
                column: "rental_file_id",
                principalTable: "rental_files",
                principalColumn: "id");
        }
    }
}
