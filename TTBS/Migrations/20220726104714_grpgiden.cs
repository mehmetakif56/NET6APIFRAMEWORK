using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class grpgiden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ad",
                table: "GidenGrup");

            migrationBuilder.RenameColumn(
                name: "GidenGrupDurumu",
                table: "GidenGrup",
                newName: "GidenGrupTamamlama");

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupDurum",
                table: "GidenGrup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupKullanma",
                table: "GidenGrup",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GidenGrupDurum",
                table: "GidenGrup");

            migrationBuilder.DropColumn(
                name: "GidenGrupKullanma",
                table: "GidenGrup");

            migrationBuilder.RenameColumn(
                name: "GidenGrupTamamlama",
                table: "GidenGrup",
                newName: "GidenGrupDurumu");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "GidenGrup",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
