using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LodgerBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableDocumentRemoveForeignKeyDocumentTypeReplaceByEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_document_type_document_type_id",
                table: "documents");

            migrationBuilder.DropTable(
                name: "document_type");

            migrationBuilder.DropIndex(
                name: "IX_documents_document_type_id",
                table: "documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documents_document_type_id",
                table: "documents",
                column: "document_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_document_type_document_type_id",
                table: "documents",
                column: "document_type_id",
                principalTable: "document_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
