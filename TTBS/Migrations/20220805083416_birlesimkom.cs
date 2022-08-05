using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class birlesimkom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BirlesimKomisyon",
                table: "BirlesimKomisyon");

            migrationBuilder.DropIndex(
                name: "IX_BirlesimKomisyon_BirlesimId",
                table: "BirlesimKomisyon");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BirlesimKomisyon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirlesimKomisyon",
                table: "BirlesimKomisyon",
                columns: new[] { "BirlesimId", "KomisyonId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BirlesimKomisyon",
                table: "BirlesimKomisyon");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BirlesimKomisyon",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BirlesimKomisyon",
                table: "BirlesimKomisyon",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BirlesimKomisyon_BirlesimId",
                table: "BirlesimKomisyon",
                column: "BirlesimId");
        }
    }
}
