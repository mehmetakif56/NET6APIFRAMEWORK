using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class Donem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DonemTarihi",
                table: "Donem",
                newName: "DonemSecTarihi");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Donem",
                newName: "MeclisKod");

            migrationBuilder.AddColumn<DateTime>(
                name: "BaslangicTarihi",
                table: "Donem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BitisTarihi",
                table: "Donem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonemAd",
                table: "Donem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonemKod",
                table: "Donem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EskiAd",
                table: "Donem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KısaAd",
                table: "Donem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MevcutUye",
                table: "Donem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UyeTamsayi",
                table: "Donem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaslangicTarihi",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "BitisTarihi",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "DonemAd",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "DonemKod",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "EskiAd",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "KısaAd",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "MevcutUye",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "UyeTamsayi",
                table: "Donem");

            migrationBuilder.RenameColumn(
                name: "MeclisKod",
                table: "Donem",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "DonemSecTarihi",
                table: "Donem",
                newName: "DonemTarihi");
        }
    }
}
