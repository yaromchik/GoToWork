using Microsoft.EntityFrameworkCore.Migrations;

namespace GoToWork_DataAccess.Migrations
{
    public partial class postUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Post");
        }
    }
}
