using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class FKBirlesim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StenoGrup",
                table: "StenoGrup");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_StenoGrup",
                table: "StenoGrup",
                columns: new[] { "StenoId", "GrupId" });
        }
    }
}
