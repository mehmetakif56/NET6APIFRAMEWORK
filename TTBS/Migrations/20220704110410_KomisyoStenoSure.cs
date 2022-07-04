using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class KomisyoStenoSure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "KomisyonStenoSure",
                table: "GorevAtama",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KomisyonStenoSure",
                table: "GorevAtama");
        }
    }
}
