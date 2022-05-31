using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class DropFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoGrup_Birlesim_BirlesimId",
                table: "StenoGrup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StenoGrup",
                table: "StenoGrup");

            migrationBuilder.DropIndex(
                name: "IX_StenoGrup_BirlesimId",
                table: "StenoGrup");

            migrationBuilder.DropColumn(
                name: "BirlesimId",
                table: "StenoGrup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StenoGrup",
                table: "StenoGrup",
                columns: new[] { "StenoId", "GrupId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StenoGrup",
                table: "StenoGrup");

            migrationBuilder.AddColumn<Guid>(
                name: "BirlesimId",
                table: "StenoGrup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StenoGrup",
                table: "StenoGrup",
                columns: new[] { "StenoId", "GrupId", "BirlesimId" });

            migrationBuilder.CreateIndex(
                name: "IX_StenoGrup_BirlesimId",
                table: "StenoGrup",
                column: "BirlesimId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoGrup_Birlesim_BirlesimId",
                table: "StenoGrup",
                column: "BirlesimId",
                principalTable: "Birlesim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
