using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StrGrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stenograf_Grup_GrupId",
                table: "Stenograf");

            migrationBuilder.DropIndex(
                name: "IX_Stenograf_GrupId",
                table: "Stenograf");
        }
    }
}
