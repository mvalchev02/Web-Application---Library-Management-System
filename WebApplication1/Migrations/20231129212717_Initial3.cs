using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivreLecteur_Books_BookId",
                table: "LivreLecteur");

            migrationBuilder.DropForeignKey(
                name: "FK_LivreLecteur_Readers_LecteurId",
                table: "LivreLecteur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LivreLecteur",
                table: "LivreLecteur");

            migrationBuilder.RenameTable(
                name: "LivreLecteur",
                newName: "ReaderBook");

            migrationBuilder.RenameIndex(
                name: "IX_LivreLecteur_LecteurId",
                table: "ReaderBook",
                newName: "IX_ReaderBook_LecteurId");

            migrationBuilder.RenameIndex(
                name: "IX_LivreLecteur_BookId",
                table: "ReaderBook",
                newName: "IX_ReaderBook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReaderBook",
                table: "ReaderBook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReaderBook_Books_BookId",
                table: "ReaderBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReaderBook_Readers_LecteurId",
                table: "ReaderBook",
                column: "LecteurId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReaderBook_Books_BookId",
                table: "ReaderBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ReaderBook_Readers_LecteurId",
                table: "ReaderBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReaderBook",
                table: "ReaderBook");

            migrationBuilder.RenameTable(
                name: "ReaderBook",
                newName: "LivreLecteur");

            migrationBuilder.RenameIndex(
                name: "IX_ReaderBook_LecteurId",
                table: "LivreLecteur",
                newName: "IX_LivreLecteur_LecteurId");

            migrationBuilder.RenameIndex(
                name: "IX_ReaderBook_BookId",
                table: "LivreLecteur",
                newName: "IX_LivreLecteur_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivreLecteur",
                table: "LivreLecteur",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LivreLecteur_Books_BookId",
                table: "LivreLecteur",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LivreLecteur_Readers_LecteurId",
                table: "LivreLecteur",
                column: "LecteurId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
