using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class gdgrp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GidenGrupDurum",
                table: "GidenGrup",
                newName: "GidenGrupMu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GidenGrupMu",
                table: "GidenGrup",
                newName: "GidenGrupDurum");
        }
    }
}
