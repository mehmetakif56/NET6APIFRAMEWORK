using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class stenoIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stenograf_UserId",
                table: "Stenograf",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stenograf_Users_UserId",
                table: "Stenograf",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stenograf_Users_UserId",
                table: "Stenograf");

            migrationBuilder.DropIndex(
                name: "IX_Stenograf_UserId",
                table: "Stenograf");
        }
    }
}
