using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoDt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BitisTarihi",
                table: "StenoPlan",
                newName: "PlanlananBitisTarihi");

            migrationBuilder.RenameColumn(
                name: "BaslangicTarihi",
                table: "StenoPlan",
                newName: "PlanlananBaslangicTarihi");

            migrationBuilder.RenameColumn(
                name: "GörevTarihi",
                table: "StenoGorev",
                newName: "GorevBitisTarihi");

            migrationBuilder.AddColumn<DateTime>(
                name: "GerceklesenBaslangicTarihi",
                table: "StenoPlan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GerceklesenBitisTarihi",
                table: "StenoPlan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GorevBasTarihi",
                table: "StenoGorev",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GerceklesenBaslangicTarihi",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "GerceklesenBitisTarihi",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "GorevBasTarihi",
                table: "StenoGorev");

            migrationBuilder.RenameColumn(
                name: "PlanlananBitisTarihi",
                table: "StenoPlan",
                newName: "BitisTarihi");

            migrationBuilder.RenameColumn(
                name: "PlanlananBaslangicTarihi",
                table: "StenoPlan",
                newName: "BaslangicTarihi");

            migrationBuilder.RenameColumn(
                name: "GorevBitisTarihi",
                table: "StenoGorev",
                newName: "GörevTarihi");
        }
    }
}
