using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class StenoGorev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StenoGorevPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StenografSayisi = table.Column<int>(type: "int", nullable: false),
                    StenografGorevSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenografGorevSure = table.Column<int>(type: "int", nullable: false),
                    UzmanStenografSayisi = table.Column<int>(type: "int", nullable: false),
                    GorevYeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GorevTuru = table.Column<int>(type: "int", nullable: false),
                    GorevAd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StenoGorevPlan", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StenoGorevPlan");
        }
    }
}
