using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class YasamaFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_Yasama_YasamaId",
                table: "StenoPlan");

            migrationBuilder.DropIndex(
                name: "IX_StenoPlan_YasamaId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "YasamaId",
                table: "StenoPlan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "YasamaId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_YasamaId",
                table: "StenoPlan",
                column: "YasamaId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_Yasama_YasamaId",
                table: "StenoPlan",
                column: "YasamaId",
                principalTable: "Yasama",
                principalColumn: "Id");
        }
    }
}
