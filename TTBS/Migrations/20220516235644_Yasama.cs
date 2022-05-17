using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class Yasama : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "YasamaId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "YasamaId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Yasama",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YasamaYili = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DonemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yasama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yasama_Donem_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_YasamaId",
                table: "StenoPlan",
                column: "YasamaId");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_YasamaId",
                table: "Birlesim",
                column: "YasamaId");

            migrationBuilder.CreateIndex(
                name: "IX_Yasama_DonemId",
                table: "Yasama",
                column: "DonemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birlesim_Yasama_YasamaId",
                table: "Birlesim",
                column: "YasamaId",
                principalTable: "Yasama",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_Yasama_YasamaId",
                table: "StenoPlan",
                column: "YasamaId",
                principalTable: "Yasama",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birlesim_Yasama_YasamaId",
                table: "Birlesim");

            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_Yasama_YasamaId",
                table: "StenoPlan");

            migrationBuilder.DropTable(
                name: "Yasama");

            migrationBuilder.DropIndex(
                name: "IX_StenoPlan_YasamaId",
                table: "StenoPlan");

            migrationBuilder.DropIndex(
                name: "IX_Birlesim_YasamaId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "YasamaId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "YasamaId",
                table: "Birlesim");
        }
    }
}
