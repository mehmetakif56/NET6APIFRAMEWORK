using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class toplamsurenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoToplamGenelSure_Stenograf_StenografId",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "StenoId",
                table: "StenoToplamGenelSure");

            migrationBuilder.AlterColumn<Guid>(
                name: "StenografId",
                table: "StenoToplamGenelSure",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StenoToplamGenelSure_Stenograf_StenografId",
                table: "StenoToplamGenelSure",
                column: "StenografId",
                principalTable: "Stenograf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StenoToplamGenelSure_Stenograf_StenografId",
                table: "StenoToplamGenelSure");

            migrationBuilder.AlterColumn<Guid>(
                name: "StenografId",
                table: "StenoToplamGenelSure",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "StenoId",
                table: "StenoToplamGenelSure",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_StenoToplamGenelSure_Stenograf_StenografId",
                table: "StenoToplamGenelSure",
                column: "StenografId",
                principalTable: "Stenograf",
                principalColumn: "Id");
        }
    }
}
