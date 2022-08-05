using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class bilsis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropIndex(
                name: "IX_Birlesim_AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropIndex(
                name: "IX_Birlesim_KomisyonId",
                table: "Birlesim");

         
            migrationBuilder.DropPrimaryKey(
                name: "PK_OzelGorev",
                table: "OzelGorev");

            migrationBuilder.DropIndex(
                name: "IX_OzelGorev_OzelGorevTurId",
                table: "OzelGorev");

            migrationBuilder.DropColumn(
                name: "AltKomisyonId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "KomisyonId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "OzelToplanmaId",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "OzelGorevTurId",
                table: "OzelGorev");

            migrationBuilder.RenameTable(
                name: "OzelGorev",
                newName: "OzelToplanma");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OzelToplanma",
                table: "OzelToplanma",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BirlesimKomisyon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KomisyonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AltKomisyonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirlesimKomisyon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BirlesimKomisyon_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BirlesimKomisyon_Komisyon_KomisyonId",
                        column: x => x.KomisyonId,
                        principalTable: "Komisyon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BirlesimOzelToplanma",
                columns: table => new
                {
                    OzelToplanmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirlesimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirlesimOzelToplanma", x => new { x.BirlesimId, x.OzelToplanmaId });
                    table.ForeignKey(
                        name: "FK_BirlesimOzelToplanma_Birlesim_BirlesimId",
                        column: x => x.BirlesimId,
                        principalTable: "Birlesim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BirlesimOzelToplanma_OzelToplanma_OzelToplanmaId",
                        column: x => x.OzelToplanmaId,
                        principalTable: "OzelToplanma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BirlesimKomisyon_BirlesimId",
                table: "BirlesimKomisyon",
                column: "BirlesimId");

            migrationBuilder.CreateIndex(
                name: "IX_BirlesimKomisyon_KomisyonId",
                table: "BirlesimKomisyon",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_BirlesimOzelToplanma_OzelToplanmaId",
                table: "BirlesimOzelToplanma",
                column: "OzelToplanmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirlesimKomisyon");

            migrationBuilder.DropTable(
                name: "BirlesimOzelToplanma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OzelToplanma",
                table: "OzelToplanma");

            migrationBuilder.RenameTable(
                name: "OzelToplanma",
                newName: "OzelGorev");

            migrationBuilder.AddColumn<Guid>(
                name: "AltKomisyonId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KomisyonId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OzelToplanmaId",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OzelGorevTurId",
                table: "OzelGorev",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OzelGorev",
                table: "OzelGorev",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_AltKomisyonId",
                table: "Birlesim",
                column: "AltKomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_KomisyonId",
                table: "Birlesim",
                column: "KomisyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Birlesim_OzelToplanmaId",
                table: "Birlesim",
                column: "OzelToplanmaId");

            migrationBuilder.CreateIndex(
                name: "IX_OzelGorev_OzelGorevTurId",
                table: "OzelGorev",
                column: "OzelGorevTurId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Birlesim_OzelGorev_OzelToplanmaId",
                table: "Birlesim",
                column: "OzelToplanmaId",
                principalTable: "OzelGorev",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OzelGorev_OzelGorevTur_OzelGorevTurId",
                table: "OzelGorev",
                column: "OzelGorevTurId",
                principalTable: "OzelGorevTur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
