using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class KomisyonToplanma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oturum_StenoPlan_StenoPlanId",
                table: "Oturum");

            migrationBuilder.DropTable(
                name: "StenoGorev");

            migrationBuilder.DropTable(
                name: "StenoPlan");

            migrationBuilder.RenameColumn(
                name: "StenoPlanId",
                table: "Oturum",
                newName: "BirlesimId");

            migrationBuilder.RenameIndex(
                name: "IX_Oturum_StenoPlanId",
                table: "Oturum",
                newName: "IX_Oturum_BirlesimId");

            migrationBuilder.CreateTable(
                name: "KomisyonToplanma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KomisYonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AltKomisYonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanlananBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanlananBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GerceklesenBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GerceklesenBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StenoSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenoSure = table.Column<int>(type: "int", nullable: false),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    Yeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomisyonToplanma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GorevAtama",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GorevBasTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevDakika = table.Column<int>(type: "int", nullable: false),
                    GorevSaniye = table.Column<int>(type: "int", nullable: false),
                    StenografId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    KomisyonToplanmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OzelToplanmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToplanmaTuru = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevAtama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GorevAtama_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtama_KomisyonToplanma_KomisyonToplanmaId",
                        column: x => x.KomisyonToplanmaId,
                        principalTable: "KomisyonToplanma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtama_OzelGorev_OzelToplanmaId",
                        column: x => x.OzelToplanmaId,
                        principalTable: "OzelGorev",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GorevAtama_Stenograf_StenografId",
                        column: x => x.StenografId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_BirlesimId",
                table: "GorevAtama",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_KomisyonToplanmaId",
                table: "GorevAtama",
                column: "KomisyonToplanmaId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_OzelToplanmaId",
                table: "GorevAtama",
                column: "OzelToplanmaId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_StenografId",
                table: "GorevAtama",
                column: "StenografId");

            migrationBuilder.AddForeignKey(
                name: "FK_Oturum_Birlesim_BirlesimId",
                table: "Oturum",
                column: "BirlesimId",
                principalTable: "Birlesim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oturum_Birlesim_BirlesimId",
                table: "Oturum");

            migrationBuilder.DropTable(
                name: "GorevAtama");

            migrationBuilder.DropTable(
                name: "KomisyonToplanma");

            migrationBuilder.RenameColumn(
                name: "BirlesimId",
                table: "Oturum",
                newName: "StenoPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Oturum_BirlesimId",
                table: "Oturum",
                newName: "IX_Oturum_StenoPlanId");

            migrationBuilder.CreateTable(
                name: "StenoPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GerceklesenBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GerceklesenBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    GorevTuru = table.Column<int>(type: "int", nullable: false),
                    GorevYeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanlananBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanlananBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StenoSayisi = table.Column<int>(type: "int", nullable: false),
                    StenoSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenoSayisi = table.Column<int>(type: "int", nullable: false),
                    UzmanStenoSure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StenoGorev",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StenografId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StenoPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GorevBasTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevDakika = table.Column<int>(type: "int", nullable: false),
                    GorevSaniye = table.Column<int>(type: "int", nullable: false),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoGorev", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StenoGorev_Stenograf_StenografId",
                        column: x => x.StenografId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StenoGorev_StenoPlan_StenoPlanId",
                        column: x => x.StenoPlanId,
                        principalTable: "StenoPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoGorev_StenografId",
                table: "StenoGorev",
                column: "StenografId");

            migrationBuilder.CreateIndex(
                name: "IX_StenoGorev_StenoPlanId",
                table: "StenoGorev",
                column: "StenoPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Oturum_StenoPlan_StenoPlanId",
                table: "Oturum",
                column: "StenoPlanId",
                principalTable: "StenoPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
