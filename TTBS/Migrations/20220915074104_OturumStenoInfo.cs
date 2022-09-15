using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class OturumStenoInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GorevAtama");

            migrationBuilder.AddColumn<int>(
                name: "AcanSira",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AcanSiraUzman",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KapatanSira",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KapatanSiraUzman",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcanSira",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "AcanSiraUzman",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "KapatanSira",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "KapatanSiraUzman",
                table: "Oturum");

            migrationBuilder.CreateTable(
                name: "GorevAtama",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StenografId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidenGrup = table.Column<bool>(type: "bit", nullable: false),
                    GorevBasTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OturumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SatırNo = table.Column<int>(type: "int", nullable: false),
                    StenoIzinTuru = table.Column<int>(type: "int", nullable: false),
                    StenoSure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SureAsmaVar = table.Column<bool>(type: "bit", nullable: false),
                    ToplantiVar = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_GorevAtama_StenografId",
                table: "GorevAtama",
                column: "StenografId");
        }
    }
}
