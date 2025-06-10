using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LodgerBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesUsersAddressesDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "document_categorie_id",
                table: "documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            // Nécessaire pour les emails identique
            migrationBuilder.Sql(@"
                UPDATE users
                SET email = email || '_' || CAST(id AS TEXT)
                WHERE email IN (
                    SELECT email
                    FROM users
                    GROUP BY email
                    HAVING COUNT(*) > 1
                );
            ");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "document_categorie_id",
                table: "documents");
        }
    }
}
