using Microsoft.EntityFrameworkCore.Migrations;

namespace Reversi.Migrations
{
    public partial class GameInDatabase5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Field",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }
    }
}
