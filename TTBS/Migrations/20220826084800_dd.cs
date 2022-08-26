using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToplantiVar",
                table: "GorevAtamaOzelToplanma",
                newName: "GidenGrupMu");

            migrationBuilder.AlterColumn<string>(
                name: "GidenGrup",
                table: "GorevAtamaOzelToplanma",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "KomisyonAd",
                table: "GorevAtamaOzelToplanma",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StenoGorevTuru",
                table: "GorevAtamaOzelToplanma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "GidenGrup",
                table: "GorevAtamaKomisyon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "GidenGrupMu",
                table: "GorevAtamaKomisyon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StenoGorevTuru",
                table: "GorevAtamaKomisyon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GidenGrup",
                table: "GorevAtamaGenelKurul",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GidenGrupMu",
                table: "GorevAtamaGenelKurul",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StenoGorevTuru",
                table: "GorevAtamaGenelKurul",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SureAsmaVar",
                table: "GorevAtamaGenelKurul",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KomisyonAd",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "StenoGorevTuru",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "GidenGrupMu",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "StenoGorevTuru",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "GidenGrup",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "GidenGrupMu",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "StenoGorevTuru",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "SureAsmaVar",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.RenameColumn(
                name: "GidenGrupMu",
                table: "GorevAtamaOzelToplanma",
                newName: "ToplantiVar");

            migrationBuilder.AlterColumn<bool>(
                name: "GidenGrup",
                table: "GorevAtamaOzelToplanma",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GidenGrup",
                table: "GorevAtamaKomisyon",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
