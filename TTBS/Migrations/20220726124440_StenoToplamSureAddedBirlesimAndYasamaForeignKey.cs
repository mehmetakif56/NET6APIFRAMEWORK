using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoToplamSureAddedBirlesimAndYasamaForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
