using Microsoft.EntityFrameworkCore.Migrations;

namespace GoToWork_DataAccess.Migrations
{
    public partial class updateTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "GuideUrl",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideUrl",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
