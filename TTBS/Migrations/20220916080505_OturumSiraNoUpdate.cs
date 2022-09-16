using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class OturumSiraNoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acan",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "AcanUzman",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "Kapatan",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "KapatanUzman",
                table: "Oturum");

            migrationBuilder.AddColumn<int>(
                name: "AcanSira",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AcanSiraUzman",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KapatanSira",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KapatanSiraUzman",
                table: "Oturum",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcanSira",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "AcanSiraUzman",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "KapatanSira",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "KapatanSiraUzman",
                table: "Oturum");

            migrationBuilder.AddColumn<string>(
                name: "Acan",
                table: "Oturum",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcanUzman",
                table: "Oturum",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kapatan",
                table: "Oturum",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KapatanUzman",
                table: "Oturum",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
