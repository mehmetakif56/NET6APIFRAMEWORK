using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class GidenGrup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_OturumId",
                table: "GorevAtama",
                column: "OturumId");

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtama_Oturum_OturumId",
                table: "GorevAtama",
                column: "OturumId",
                principalTable: "Oturum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
