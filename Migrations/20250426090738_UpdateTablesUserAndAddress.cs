using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LodgerBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesUserAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_rental_files_address_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "rental_file_id",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_users_rental_file_id",
                table: "users",
                column: "rental_file_id");

            // Nécessaire car rattaché à l'id address FK_users_rental_files_address_id 
            migrationBuilder.Sql(@"
                UPDATE users
                SET rental_file_id = NULL
                WHERE rental_file_id IS NOT NULL
                AND rental_file_id NOT IN (SELECT id FROM rental_files);
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_users_rental_files_rental_file_id",
                table: "users",
                column: "rental_file_id",
                principalTable: "rental_files",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull); 
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_rental_files_address_id",
                table: "users",
                column: "address_id",
                principalTable: "rental_files",
                principalColumn: "id");
        }
    }
}
