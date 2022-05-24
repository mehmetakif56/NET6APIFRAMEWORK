using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class AltKomiyon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AltKomisyon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KomisyonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltKomisyon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AltKomisyon_Komisyon_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltKomisyon_KomisyonId",
                table: "AltKomisyon",
                column: "KomisyonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltKomisyon");
        }
    }
}
