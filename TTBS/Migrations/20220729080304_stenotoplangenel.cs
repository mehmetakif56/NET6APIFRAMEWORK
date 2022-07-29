using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class stenotoplangenel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoToplamGenelSure_Birlesim_BirlesimId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropForeignKey(
                name: "FK_StenoToplamGenelSure_Yasama_YasamaId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropIndex(
                name: "IX_StenoToplamGenelSure_BirlesimId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropIndex(
                name: "IX_StenoToplamGenelSure_YasamaId",
                table: "StenoToplamGenelSure");

            migrationBuilder.AddColumn<string>(
                name: "BirlesimAd",
                table: "StenoToplamGenelSure",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StenografId",
                table: "StenoToplamGenelSure",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YasamaAd",
                table: "StenoToplamGenelSure",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StenoToplamGenelSure_StenografId",
                table: "StenoToplamGenelSure",
                column: "StenografId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoToplamGenelSure_Stenograf_StenografId",
                table: "StenoToplamGenelSure",
                column: "StenografId",
                principalTable: "Stenograf",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoToplamGenelSure_Stenograf_StenografId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropIndex(
                name: "IX_StenoToplamGenelSure_StenografId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "BirlesimAd",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "StenografId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "YasamaAd",
                table: "StenoToplamGenelSure");

            migrationBuilder.CreateIndex(
                name: "IX_StenoToplamGenelSure_BirlesimId",
                table: "StenoToplamGenelSure",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_StenoToplamGenelSure_YasamaId",
                table: "StenoToplamGenelSure",
                column: "YasamaId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoToplamGenelSure_Birlesim_BirlesimId",
                table: "StenoToplamGenelSure",
                column: "BirlesimId",
                principalTable: "Birlesim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StenoToplamGenelSure_Yasama_YasamaId",
                table: "StenoToplamGenelSure",
                column: "YasamaId",
                principalTable: "Yasama",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
