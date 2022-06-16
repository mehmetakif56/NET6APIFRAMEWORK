using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class BirleimKomm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GorevAtama_KomisyonToplanma_KomisyonToplanmaId",
                table: "GorevAtama");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevAtama_OzelGorev_OzelToplanmaId",
                table: "GorevAtama");

            migrationBuilder.DropTable(
                name: "KomisyonToplanma");

            migrationBuilder.DropIndex(
                name: "IX_GorevAtama_KomisyonToplanmaId",
                table: "GorevAtama");

            migrationBuilder.DropIndex(
                name: "IX_GorevAtama_OzelToplanmaId",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "KomisyonToplanmaId",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "ToplanmaTuru",
                table: "GorevAtama");

            migrationBuilder.RenameColumn(
                name: "OzelToplanmaId",
                table: "GorevAtama",
                newName: "OturumId");

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

            migrationBuilder.AddColumn<int>(
                name: "ToplanmaTuru",
                table: "Birlesim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurAdedi",
                table: "Birlesim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Yeri",
                table: "Birlesim",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "KomisyonId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "ToplanmaTuru",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "TurAdedi",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "Yeri",
                table: "Birlesim");

            migrationBuilder.RenameColumn(
                name: "OturumId",
                table: "GorevAtama",
                newName: "OzelToplanmaId");

            migrationBuilder.AddColumn<Guid>(
                name: "KomisyonToplanmaId",
                table: "GorevAtama",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ToplanmaTuru",
                table: "GorevAtama",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KomisyonToplanma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AltKomisYonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GerceklesenBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GerceklesenBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevStatu = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KomisYonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanlananBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanlananBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StenoSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenoSure = table.Column<int>(type: "int", nullable: false),
                    Yeri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomisyonToplanma", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_KomisyonToplanmaId",
                table: "GorevAtama",
                column: "KomisyonToplanmaId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevAtama_OzelToplanmaId",
                table: "GorevAtama",
                column: "OzelToplanmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtama_KomisyonToplanma_KomisyonToplanmaId",
                table: "GorevAtama",
                column: "KomisyonToplanmaId",
                principalTable: "KomisyonToplanma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevAtama_OzelGorev_OzelToplanmaId",
                table: "GorevAtama",
                column: "OzelToplanmaId",
                principalTable: "OzelGorev",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
