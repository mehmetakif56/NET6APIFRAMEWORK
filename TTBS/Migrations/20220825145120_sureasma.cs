using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class sureasma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Yasama");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Yasama");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Yasama");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Yasama");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Yasama");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRoleEntity");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserRoleEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserRoleEntity");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserRoleEntity");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UserRoleEntity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "StenoToplamGenelSure");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "StenoIzin");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StenografBeklemeSure");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StenografBeklemeSure");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StenografBeklemeSure");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "StenografBeklemeSure");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "StenografBeklemeSure");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Stenograf");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RoleEntity");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RoleEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoleEntity");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "RoleEntity");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "RoleEntity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OzelToplanma");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OzelToplanma");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OzelToplanma");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "OzelToplanma");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "OzelToplanma");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OzelGorevTur");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OzelGorevTur");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OzelGorevTur");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "OzelGorevTur");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "OzelGorevTur");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Oturum");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Komisyon");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Komisyon");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Komisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Komisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Komisyon");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GrupDetay");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GorevAtamaOzelToplanma");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GorevAtamaKomisyon");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "SureAsmaVar",
                table: "GorevAtamaGenelKurul");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GorevAtama");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Donem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BirlesimOzelToplanma");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BirlesimOzelToplanma");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BirlesimOzelToplanma");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "BirlesimOzelToplanma");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BirlesimOzelToplanma");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BirlesimKomisyon");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BirlesimKomisyon");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BirlesimKomisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "BirlesimKomisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BirlesimKomisyon");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Birlesim");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AltKomisyon");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AltKomisyon");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AltKomisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AltKomisyon");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "AltKomisyon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Yasama",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Yasama",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Yasama",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Yasama",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Yasama",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserRoleEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserRoleEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserRoleEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "UserRoleEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "UserRoleEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "StenoToplamGenelSure",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StenoToplamGenelSure",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StenoToplamGenelSure",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "StenoToplamGenelSure",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "StenoToplamGenelSure",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "StenoIzin",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StenoIzin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StenoIzin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "StenoIzin",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "StenoIzin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "StenografBeklemeSure",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StenografBeklemeSure",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StenografBeklemeSure",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "StenografBeklemeSure",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "StenografBeklemeSure",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Stenograf",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Stenograf",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stenograf",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Stenograf",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Stenograf",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "RoleEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RoleEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoleEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "RoleEntity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "RoleEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "RoleClaims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RoleClaims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoleClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "RoleClaims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "RoleClaims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "OzelToplanma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OzelToplanma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OzelToplanma",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "OzelToplanma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "OzelToplanma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "OzelGorevTur",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OzelGorevTur",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OzelGorevTur",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "OzelGorevTur",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "OzelGorevTur",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Oturum",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Oturum",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Oturum",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Oturum",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Oturum",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Komisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Komisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Komisyon",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Komisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Komisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "GrupDetay",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GrupDetay",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GrupDetay",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "GrupDetay",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GrupDetay",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Grup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Grup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Grup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Grup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Grup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "GorevAtamaOzelToplanma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GorevAtamaOzelToplanma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GorevAtamaOzelToplanma",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "GorevAtamaOzelToplanma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GorevAtamaOzelToplanma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "GorevAtamaKomisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GorevAtamaKomisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GorevAtamaKomisyon",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "GorevAtamaKomisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GorevAtamaKomisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "GorevAtamaGenelKurul",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GorevAtamaGenelKurul",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GorevAtamaGenelKurul",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "GorevAtamaGenelKurul",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GorevAtamaGenelKurul",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SureAsmaVar",
                table: "GorevAtamaGenelKurul",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "GorevAtama",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GorevAtama",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GorevAtama",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "GorevAtama",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GorevAtama",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Donem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Donem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Donem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Donem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Donem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "BirlesimOzelToplanma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BirlesimOzelToplanma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BirlesimOzelToplanma",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "BirlesimOzelToplanma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BirlesimOzelToplanma",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "BirlesimKomisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BirlesimKomisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BirlesimKomisyon",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "BirlesimKomisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BirlesimKomisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Birlesim",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Birlesim",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Birlesim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Birlesim",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "AltKomisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AltKomisyon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AltKomisyon",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "AltKomisyon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "AltKomisyon",
                type: "datetime2",
                nullable: true);
        }
    }
}
