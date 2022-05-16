using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class PlanStatu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.RenameColumn(
                name: "GorevSuresi",
                table: "StenoGorev",
                newName: "GorevSaniye");

            migrationBuilder.RenameColumn(
                name: "GorevSaati",
                table: "StenoGorev",
                newName: "GorevDakika");

            migrationBuilder.AlterColumn<Guid>(
                name: "GorevTuruId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "PlanStatu",
                table: "StenoPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan",
                column: "GorevTuruId",
                principalTable: "GorevTuru",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan");

            migrationBuilder.DropColumn(
                name: "PlanStatu",
                table: "StenoPlan");

            migrationBuilder.RenameColumn(
                name: "GorevSaniye",
                table: "StenoGorev",
                newName: "GorevSuresi");

            migrationBuilder.RenameColumn(
                name: "GorevDakika",
                table: "StenoGorev",
                newName: "GorevSaati");

            migrationBuilder.AlterColumn<Guid>(
                name: "GorevTuruId",
                table: "StenoPlan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StenoPlan_GorevTuru_GorevTuruId",
                table: "StenoPlan",
                column: "GorevTuruId",
                principalTable: "GorevTuru",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
