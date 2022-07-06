using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoSure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KomisyonStenoSure",
                table: "GorevAtama",
                newName: "StenoSure");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StenoSure",
                table: "GorevAtama",
                newName: "KomisyonStenoSure");
        }
    }
}
