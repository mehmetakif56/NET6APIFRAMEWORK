using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class birlesimKapatan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BirlesimKapatanMi",
                table: "Stenograf",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirlesimKapatanMi",
                table: "Stenograf");
        }
    }
}
