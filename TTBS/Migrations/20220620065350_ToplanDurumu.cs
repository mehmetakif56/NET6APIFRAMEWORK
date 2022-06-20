using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class ToplanDurumu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToplanmaDurumu",
                table: "Birlesim",
                type: "int",
                nullable: false,
                defaultValue: 0);

       }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToplanmaDurumu",
                table: "Birlesim");
        }
    }
}
