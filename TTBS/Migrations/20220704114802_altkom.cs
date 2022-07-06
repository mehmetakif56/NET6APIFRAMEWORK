using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class altkom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_AltKomisyonId",
                table: "Birlesim",
                column: "AltKomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_KomisyonId",
                table: "Birlesim",
                column: "KomisyonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birlesim_AltKomisyon_AltKomisyonId",
                table: "Birlesim",
                column: "AltKomisyonId",
                principalTable: "AltKomisyon",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Birlesim_Komisyon_KomisyonId",
                table: "Birlesim",
                column: "KomisyonId",
                principalTable: "Komisyon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birlesim_AltKomisyon_AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropForeignKey(
                name: "FK_Birlesim_Komisyon_KomisyonId",
                table: "Birlesim");
            migrationBuilder.DropIndex(
                name: "IX_Birlesim_AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropIndex(
                name: "IX_Birlesim_KomisyonId",
                table: "Birlesim");

        }
    }
}
