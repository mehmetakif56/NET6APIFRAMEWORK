using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class removeBeklemeSüre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeklemeSure",
                table: "Birlesim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BeklemeSure",
                table: "Birlesim",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
