using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class stenotoplam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YasamaAd",
                table: "StenoToplamGenelSure");

            migrationBuilder.CreateIndex(
                name: "IX_StenoToplamGenelSure_BirlesimId",
                table: "StenoToplamGenelSure",
                column: "BirlesimId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StenoToplamGenelSure_BirlesimId",
                table: "StenoToplamGenelSure");

            migrationBuilder.AddColumn<string>(
                name: "YasamaAd",
                table: "StenoToplamGenelSure",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
