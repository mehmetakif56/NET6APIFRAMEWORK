using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class GorevTuruRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.DropTable(
                name: "GorevTuru");

            migrationBuilder.DropIndex(
                name: "IX_StenoPlan_GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.AddColumn<int>(
                name: "PlanTuru",
                table: "StenoPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanTuru",
                table: "StenoPlan");

            migrationBuilder.AddColumn<Guid>(
                name: "GorevTuruId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GorevTuru",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GorevTuru", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StenoPlan_GorevTuruId",
                table: "StenoPlan",
                column: "GorevTuruId");

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan",
                column: "GorevTuruId",
                principalTable: "GorevTuru",
                principalColumn: "Id");
        }
    }
}
