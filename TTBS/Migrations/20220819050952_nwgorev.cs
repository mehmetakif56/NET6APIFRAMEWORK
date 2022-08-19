using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class nwgorev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GorevSaniye",
                table: "GorevAtama",
                newName: "StenoIzinTuru");

            migrationBuilder.RenameColumn(
                name: "GorevDakika",
                table: "GorevAtama",
                newName: "SatırNo");

            migrationBuilder.AddColumn<bool>(
                name: "GidenGrup",
                table: "GorevAtama",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SureAsmaVar",
                table: "GorevAtama",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToplantiVar",
                table: "GorevAtama",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GorevAtamaGenelKurul",
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
                    ToplantiVar = table.Column<bool>(type: "bit", nullable: false),
                    GidenGrup = table.Column<bool>(type: "bit", nullable: false),
                    StenoIzinTuru = table.Column<int>(type: "int", nullable: false),
                    SureAsmaVar = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaGenelKurul", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevAtamaGenelKurul_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaGenelKurul_Stenograf_StenografId",
                        column: x => x.StenografId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevAtamaKomisyon",
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
                    ToplantiVar = table.Column<bool>(type: "bit", nullable: false),
                    GidenGrup = table.Column<bool>(type: "bit", nullable: false),
                    StenoIzinTuru = table.Column<int>(type: "int", nullable: false),
                    SureAsmaVar = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaKomisyon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevAtamaKomisyon_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaKomisyon_Stenograf_StenografId",
                        column: x => x.StenografId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GorevAtamaOzelToplanma",
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
                    ToplantiVar = table.Column<bool>(type: "bit", nullable: false),
                    GidenGrup = table.Column<bool>(type: "bit", nullable: false),
                    StenoIzinTuru = table.Column<int>(type: "int", nullable: false),
                    SureAsmaVar = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtamaOzelToplanma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevAtamaOzelToplanma_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtamaOzelToplanma_Stenograf_StenografId",
                        column: x => x.StenografId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaGenelKurul_BirlesimId",
                table: "GorevAtamaGenelKurul",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaGenelKurul_StenografId",
                table: "GorevAtamaGenelKurul",
                column: "StenografId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKomisyon_BirlesimId",
                table: "GorevAtamaKomisyon",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaKomisyon_StenografId",
                table: "GorevAtamaKomisyon",
                column: "StenografId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaOzelToplanma_BirlesimId",
                table: "GorevAtamaOzelToplanma",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtamaOzelToplanma_StenografId",
                table: "GorevAtamaOzelToplanma",
                column: "StenografId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GorevAtamaGenelKurul");

            migrationBuilder.DropTable(
                name: "GorevAtamaKomisyon");

            migrationBuilder.DropTable(
                name: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "GidenGrup",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "SureAsmaVar",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "ToplantiVar",
                table: "GorevAtama");

            migrationBuilder.RenameColumn(
                name: "StenoIzinTuru",
                table: "GorevAtama",
                newName: "GorevSaniye");

            migrationBuilder.RenameColumn(
                name: "SatırNo",
                table: "GorevAtama",
                newName: "GorevDakika");
        }
    }
}
