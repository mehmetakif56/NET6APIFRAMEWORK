using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class nw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirlesimKomisyon");

            migrationBuilder.DropTable(
                name: "BirlesimOzelToplanma");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GidenGrupSaat",
                table: "GrupDetay",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AltKomisyonId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KomisyonId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OzelToplanmaId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_AltKomisyonId",
                table: "Birlesim",
                column: "AltKomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_KomisyonId",
                table: "Birlesim",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_OzelToplanmaId",
                table: "Birlesim",
                column: "OzelToplanmaId");

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birlesim_AltKomisyon_AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropForeignKey(
                name: "FK_Birlesim_Komisyon_KomisyonId",
                table: "Birlesim");

            migrationBuilder.DropForeignKey(
                name: "FK_Birlesim_OzelToplanma_OzelToplanmaId",
                table: "Birlesim");

            migrationBuilder.DropIndex(
                name: "IX_Birlesim_AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropIndex(
                name: "IX_Birlesim_KomisyonId",
                table: "Birlesim");

            migrationBuilder.DropIndex(
                name: "IX_Birlesim_OzelToplanmaId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "KomisyonId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "OzelToplanmaId",
                table: "Birlesim");

            migrationBuilder.AlterColumn<string>(
                name: "GidenGrupSaat",
                table: "GrupDetay",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BirlesimKomisyon",
                columns: table => new
                {
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KomisyonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AltKomisyonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirlesimKomisyon", x => new { x.BirlesimId, x.KomisyonId });
                    table.ForeignKey(
                        name: "FK_BirlesimKomisyon_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BirlesimKomisyon_Komisyon_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BirlesimOzelToplanma",
                columns: table => new
                {
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OzelToplanmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirlesimOzelToplanma", x => new { x.BirlesimId, x.OzelToplanmaId });
                    table.ForeignKey(
                        name: "FK_BirlesimOzelToplanma_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BirlesimOzelToplanma_OzelToplanma_OzelToplanmaId",
                        column: x => x.OzelToplanmaId,
                        principalTable: "OzelToplanma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BirlesimKomisyon_KomisyonId",
                table: "BirlesimKomisyon",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_BirlesimOzelToplanma_OzelToplanmaId",
                table: "BirlesimOzelToplanma",
                column: "OzelToplanmaId");
        }
    }
}
