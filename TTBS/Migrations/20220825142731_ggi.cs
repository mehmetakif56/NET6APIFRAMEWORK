using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class ggi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GidenGrup",
                table: "GorevAtamaGenelKurul");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GidenGrup",
                table: "GorevAtamaGenelKurul",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
