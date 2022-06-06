using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class Oturum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oturum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OturumNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OturumBaskan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KatipUye_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KatipUye_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KapaliOturum = table.Column<bool>(type: "bit", nullable: false),
                    YoklamaliMi = table.Column<bool>(type: "bit", nullable: false),
                    StenoPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oturum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oturum_StenoPlan_StenoPlanId",
                        column: x => x.StenoPlanId,
                        principalTable: "StenoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oturum_StenoPlanId",
                table: "Oturum",
                column: "StenoPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oturum");
        }
    }
}
