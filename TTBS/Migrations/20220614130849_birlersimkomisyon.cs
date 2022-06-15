using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class birlersimkomisyon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_Birlesim_BirlesimId",
                table: "StenoPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_Komisyon_KomisyonId",
                table: "StenoPlan");

            migrationBuilder.DropIndex(
                name: "IX_StenoPlan_BirlesimId",
                table: "StenoPlan");

            migrationBuilder.DropIndex(
                name: "IX_StenoPlan_KomisyonId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "BirlesimId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "KomisyonId",
                table: "StenoPlan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BirlesimId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KomisyonId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_BirlesimId",
                table: "StenoPlan",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_KomisyonId",
                table: "StenoPlan",
                column: "KomisyonId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_Birlesim_BirlesimId",
                table: "StenoPlan",
                column: "BirlesimId",
                principalTable: "Birlesim",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_Komisyon_KomisyonId",
                table: "StenoPlan",
                column: "KomisyonId",
                principalTable: "Komisyon",
                principalColumn: "Id");
        }
    }
}
