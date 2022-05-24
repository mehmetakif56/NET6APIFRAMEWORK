using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class al : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AltKomisyon_Komisyon_KomisyonId",
                table: "AltKomisyon");

            migrationBuilder.AlterColumn<Guid>(
                name: "KomisyonId",
                table: "AltKomisyon",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AltKomisyon_Komisyon_KomisyonId",
                table: "AltKomisyon",
                column: "KomisyonId",
                principalTable: "Komisyon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AltKomisyon_Komisyon_KomisyonId",
                table: "AltKomisyon");

            migrationBuilder.AlterColumn<Guid>(
                name: "KomisyonId",
                table: "AltKomisyon",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AltKomisyon_Komisyon_KomisyonId",
                table: "AltKomisyon",
                column: "KomisyonId",
                principalTable: "Komisyon",
                principalColumn: "Id");
        }
    }
}
