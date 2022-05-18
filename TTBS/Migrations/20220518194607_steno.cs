using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class steno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "StenoGorev");

            migrationBuilder.RenameColumn(
                name: "PlanTuru",
                table: "StenoPlan",
                newName: "GorevTuru");

            migrationBuilder.RenameColumn(
                name: "PlanStatu",
                table: "StenoPlan",
                newName: "GorevStatu");

            migrationBuilder.RenameColumn(
                name: "IzınTuru",
                table: "StenoIzin",
                newName: "IzinTuru");

            migrationBuilder.AddColumn<Guid>(
                name: "StenografId",
                table: "StenoIzin",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StenografId",
                table: "StenoGorev",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Stenograf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StenoGorevTuru = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stenograf", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoIzin_StenografId",
                table: "StenoIzin",
                column: "StenografId");

            migrationBuilder.CreateIndex(
                name: "IX_StenoGorev_StenografId",
                table: "StenoGorev",
                column: "StenografId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoGorev_Stenograf_StenografId",
                table: "StenoGorev",
                column: "StenografId",
                principalTable: "Stenograf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StenoIzin_Stenograf_StenografId",
                table: "StenoIzin",
                column: "StenografId",
                principalTable: "Stenograf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoGorev_Stenograf_StenografId",
                table: "StenoGorev");

            migrationBuilder.DropForeignKey(
                name: "FK_StenoIzin_Stenograf_StenografId",
                table: "StenoIzin");

            migrationBuilder.DropTable(
                name: "Stenograf");

            migrationBuilder.DropIndex(
                name: "IX_StenoIzin_StenografId",
                table: "StenoIzin");

            migrationBuilder.DropIndex(
                name: "IX_StenoGorev_StenografId",
                table: "StenoGorev");

            migrationBuilder.DropColumn(
                name: "StenografId",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "StenografId",
                table: "StenoGorev");

            migrationBuilder.RenameColumn(
                name: "GorevTuru",
                table: "StenoPlan",
                newName: "PlanTuru");

            migrationBuilder.RenameColumn(
                name: "GorevStatu",
                table: "StenoPlan",
                newName: "PlanStatu");

            migrationBuilder.RenameColumn(
                name: "IzinTuru",
                table: "StenoIzin",
                newName: "IzınTuru");

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "StenoIzin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "StenoGorev",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
