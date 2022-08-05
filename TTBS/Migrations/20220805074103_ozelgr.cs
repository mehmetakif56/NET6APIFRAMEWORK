using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class ozelgr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OzelGorevTurId",
                table: "OzelToplanma",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OzelToplanma_OzelGorevTurId",
                table: "OzelToplanma",
                column: "OzelGorevTurId");

            migrationBuilder.AddForeignKey(
                name: "FK_OzelToplanma_OzelGorevTur_OzelGorevTurId",
                table: "OzelToplanma",
                column: "OzelGorevTurId",
                principalTable: "OzelGorevTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OzelToplanma_OzelGorevTur_OzelGorevTurId",
                table: "OzelToplanma");

            migrationBuilder.DropIndex(
                name: "IX_OzelToplanma_OzelGorevTurId",
                table: "OzelToplanma");

            migrationBuilder.DropColumn(
                name: "OzelGorevTurId",
                table: "OzelToplanma");
        }
    }
}
