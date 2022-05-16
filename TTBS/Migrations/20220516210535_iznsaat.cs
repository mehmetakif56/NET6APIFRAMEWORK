using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class iznsaat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaslangicSaati",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "BitisSaati",
                table: "StenoIzin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaslangicSaati",
                table: "StenoIzin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BitisSaati",
                table: "StenoIzin",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
