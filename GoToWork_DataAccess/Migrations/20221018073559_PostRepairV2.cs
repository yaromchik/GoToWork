using Microsoft.EntityFrameworkCore.Migrations;

namespace GoToWork_DataAccess.Migrations
{
    public partial class PostRepairV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
