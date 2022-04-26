using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoBirlesimKomisyon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StenoGorevPlan");

            migrationBuilder.CreateTable(
                name: "Birlesim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirlesimNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birlesim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Komisyon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Yeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KomisyonTipi = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisyon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StenoPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StenoSayisi = table.Column<int>(type: "int", nullable: false),
                    StenoSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenoSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenoSayisi = table.Column<int>(type: "int", nullable: false),
                    Yeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToplanmaTuru = table.Column<int>(type: "int", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KomisyonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StenoPlan_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StenoPlan_Komisyon_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_BirlesimId",
                table: "StenoPlan",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_KomisyonId",
                table: "StenoPlan",
                column: "KomisyonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StenoPlan");

            migrationBuilder.DropTable(
                name: "Birlesim");

            migrationBuilder.DropTable(
                name: "Komisyon");

            migrationBuilder.CreateTable(
                name: "StenoGorevPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GorevTuru = table.Column<int>(type: "int", nullable: false),
                    GorevYeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StenografGorevSure = table.Column<int>(type: "int", nullable: false),
                    StenografSayisi = table.Column<int>(type: "int", nullable: false),
                    UzmanStenografGorevSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenografSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoGorevPlan", x => x.Id);
                });
        }
    }
}
