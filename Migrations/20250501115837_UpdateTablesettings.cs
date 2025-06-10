using LodgerBackend.src.App.Settings.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LodgerBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Met à jour les anciennes valeurs non numériques pour les définir à 0
            migrationBuilder.Sql("UPDATE settings SET theme = '0' WHERE theme IS NULL OR theme = '' OR theme !~ '^\\d+$';");

            // Utilisation d'une requête SQL pour modifier le type de la colonne de manière explicite
            migrationBuilder.Sql("ALTER TABLE settings ALTER COLUMN theme TYPE integer USING (theme::integer);");

            // Ajout de la valeur par défaut de 0
            migrationBuilder.Sql("ALTER TABLE settings ALTER COLUMN theme SET DEFAULT 0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE settings ALTER COLUMN theme TYPE text USING (theme::text);");

            migrationBuilder.AlterColumn<string>(
                name: "theme",
                table: "settings",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
