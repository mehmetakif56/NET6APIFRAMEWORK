using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoGrupFKEY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoGrup_Grup_StenoId",
                table: "StenoGrup");

            migrationBuilder.CreateIndex(
                name: "IX_StenoGrup_GrupId",
                table: "StenoGrup",
                column: "GrupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoGrup_Grup_GrupId",
                table: "StenoGrup",
                column: "GrupId",
                principalTable: "Grup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoGrup_Grup_GrupId",
                table: "StenoGrup");

            migrationBuilder.DropIndex(
                name: "IX_StenoGrup_GrupId",
                table: "StenoGrup");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoGrup_Grup_StenoId",
                table: "StenoGrup",
                column: "StenoId",
                principalTable: "Grup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
