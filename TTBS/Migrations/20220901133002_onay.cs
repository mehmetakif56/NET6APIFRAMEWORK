using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class onay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnayDurumu",
                table: "GorevAtamaOzelToplanma",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnayDurumu",
                table: "GorevAtamaKomisyon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnayDurumu",
                table: "GorevAtamaGenelKurul",



                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GorevAtamaKomisyonOnay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SatırNo = table.Column<int>(type: "int", nullable: false),
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StenografId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OturumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GorevBasTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StenoSure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    KomisyonAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StenoIzinTuru = table.Column<int>(type: "int", nullable: false),
                    SureAsmaVar = table.Column<bool>(type: "bit", nullable: false),
                    OnayDurumu = table.Column<bool>(type: "bit", nullable: false),
                    GidenGrup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StenoGorevTuru = table.Column<int>(type: "int", nullable: false),
                    GidenGrupMu = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaKomisyonOnay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevAtamaKomisyonOnay_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaKomisyonOnay_Stenograf_StenografId",
                        column: x => x.StenografId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKomisyonOnay_BirlesimId",
                table: "GorevAtamaKomisyonOnay",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKomisyonOnay_StenografId",
                table: "GorevAtamaKomisyonOnay",
                column: "StenografId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GorevAtamaKomisyonOnay");

            migrationBuilder.DropColumn(
                name: "OnayDurumu",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "OnayDurumu",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "OnayDurumu",
                table: "GorevAtamaGenelKurul");
        }
    }
}
