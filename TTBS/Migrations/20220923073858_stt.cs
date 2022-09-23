﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class stt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirlesimKapat",
                table: "Stenograf",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "BirlesimKapatan",
                table: "Stenograf",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirlesimKapat",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "BirlesimKapatan",
                table: "Stenograf");
        }
    }
}
