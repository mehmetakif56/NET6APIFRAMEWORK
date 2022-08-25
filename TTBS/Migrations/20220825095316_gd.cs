using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class gd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToplantiVar",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "ToplantiVar",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.AddColumn<string>(
                name: "GidenGrupSaat",
                table: "Stenograf",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KomisyonAd",
                table: "GorevAtamaKomisyon",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SureAsmaVar",
                table: "GorevAtamaGenelKurul",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "GidenGrup",
                table: "GorevAtamaGenelKurul",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "KomisyonAd",
                table: "GorevAtamaGenelKurul",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GidenGrupSaat",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "KomisyonAd",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "KomisyonAd",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.AddColumn<bool>(
                name: "ToplantiVar",
                table: "GorevAtamaKomisyon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "SureAsmaVar",
                table: "GorevAtamaGenelKurul",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "GidenGrup",
                table: "GorevAtamaGenelKurul",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "ToplantiVar",
                table: "GorevAtamaGenelKurul",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
