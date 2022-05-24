using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class Stenosiran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stenograf_Grup_GrupId",
                table: "Stenograf");

            migrationBuilder.DropIndex(
                name: "IX_Stenograf_GrupId",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "GrupId",
                table: "Stenograf");

            migrationBuilder.AddColumn<string>(
                name: "SiraNo",
                table: "Stenograf",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StenoGrup",
                columns: table => new
                {
                    StenoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoGrup", x => new { x.StenoId, x.GrupId });
                    table.ForeignKey(
                        name: "FK_StenoGrup_Grup_GrupId",
                        column: x => x.GrupId,
                        principalTable: "Grup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StenoGrup_Stenograf_StenoId",
                        column: x => x.StenoId,
                        principalTable: "Stenograf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoGrup_GrupId",
                table: "StenoGrup",
                column: "GrupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StenoGrup");

            migrationBuilder.DropColumn(
                name: "SiraNo",
                table: "Stenograf");

            migrationBuilder.AddColumn<Guid>(
                name: "GrupId",
                table: "Stenograf",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stenograf_GrupId",
                table: "Stenograf",
                column: "GrupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stenograf_Grup_GrupId",
                table: "Stenograf",
                column: "GrupId",
                principalTable: "Grup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
