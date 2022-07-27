using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class grrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GidenGrupDurumu",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "GidenGrupNo",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "GidenGrupTarihi",
                table: "Grup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GidenGrupDurumu",
                table: "Grup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GidenGrupNo",
                table: "Grup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "GidenGrupTarihi",
                table: "Grup",
                type: "datetime2",
                nullable: true);
        }
    }
}
