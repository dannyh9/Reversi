using Microsoft.EntityFrameworkCore.Migrations;

namespace Reversi.Migrations
{
    public partial class GameInDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Player2",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Player2",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
