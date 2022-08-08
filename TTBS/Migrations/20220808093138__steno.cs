using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class _steno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StenoGrup");

            migrationBuilder.AddColumn<Guid>(
                name: "GrupId",
                table: "Stenograf",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stenograf_GrupId",
                table: "Stenograf",
                column: "GrupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stenograf_Grup_GrupId",
                table: "Stenograf",
                column: "GrupId",
                principalTable: "Grup",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "StenoGrup",
                columns: table => new
                {
                    StenoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GidenGrupMu = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
    }
}
