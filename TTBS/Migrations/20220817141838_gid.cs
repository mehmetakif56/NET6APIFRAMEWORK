using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class gid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GidenGrupTarihi",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "GidenGrupMu",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "GidenGrupPasif",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "GidenGrupSaat",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "GidenGrupSaatUygula",
                table: "Grup");

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupMu",
                table: "GrupDetay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupPasif",
                table: "GrupDetay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GidenGrupSaat",
                table: "GrupDetay",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupSaatUygula",
                table: "GrupDetay",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GidenGrupMu",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "GidenGrupPasif",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "GidenGrupSaat",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "GidenGrupSaatUygula",
                table: "GrupDetay");

            migrationBuilder.AddColumn<DateTime>(
                name: "GidenGrupTarihi",
                table: "GrupDetay",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupMu",
                table: "Grup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupPasif",
                table: "Grup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GidenGrupSaat",
                table: "Grup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupSaatUygula",
                table: "Grup",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
