using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoGorevPl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToplanmaTuru",
                table: "StenoPlan");

            migrationBuilder.RenameColumn(
                name: "Yeri",
                table: "StenoPlan",
                newName: "GorevYeri");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "StenoPlan",
                newName: "GorevAd");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Donem",
                newName: "Ad");

            migrationBuilder.AddColumn<Guid>(
                name: "GorevTuruId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GorevTuru",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StenoGorev",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GörevTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevSaati = table.Column<int>(type: "int", nullable: false),
                    GorevSuresi = table.Column<int>(type: "int", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StenoPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoGorev", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StenoGorev_StenoPlan_StenoPlanId",
                        column: x => x.StenoPlanId,
                        principalTable: "StenoPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_GorevTuruId",
                table: "StenoPlan",
                column: "GorevTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_StenoGorev_StenoPlanId",
                table: "StenoGorev",
                column: "StenoPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan",
                column: "GorevTuruId",
                principalTable: "GorevTuru",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.DropTable(
                name: "GorevTuru");

            migrationBuilder.DropTable(
                name: "StenoGorev");

            migrationBuilder.DropIndex(
                name: "IX_StenoPlan_GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.RenameColumn(
                name: "GorevYeri",
                table: "StenoPlan",
                newName: "Yeri");

            migrationBuilder.RenameColumn(
                name: "GorevAd",
                table: "StenoPlan",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Donem",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "ToplanmaTuru",
                table: "StenoPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
