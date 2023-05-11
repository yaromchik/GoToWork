using Microsoft.EntityFrameworkCore.Migrations;

namespace GoToWork_DataAccess.Migrations
{
    public partial class ForKeyLauguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Post_LanguageId",
                table: "Post",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Language_LanguageId",
                table: "Post",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Language_LanguageId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_LanguageId",
                table: "Post");
        }
    }
}
