using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class Planturu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanTuru",
                table: "StenografBeklemeSure",
                newName: "ToplanmaTuru");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToplanmaTuru",
                table: "StenografBeklemeSure",
                newName: "PlanTuru");
        }
    }
}
