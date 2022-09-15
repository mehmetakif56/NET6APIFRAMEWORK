﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTBS.Infrastructure;

#nullable disable

namespace TTBS.Migrations
{
    [DbContext(typeof(TTBSContext))]
    partial class TTBSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TTBS.Core.Entities.AltKomisyon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("KomisyonId");

                    b.ToTable("AltKomisyon", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Birlesim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AltKomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirlesimNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OzelToplanmaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("StenoSure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ToplanmaDurumu")
                        .HasColumnType("int");

                    b.Property<int>("ToplanmaTuru")
                        .HasColumnType("int");

                    b.Property<int>("TurAdedi")
                        .HasColumnType("int");

                    b.Property<decimal>("UzmanStenoSure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("YasamaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Yeri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AltKomisyonId");

                    b.HasIndex("KomisyonId");

                    b.HasIndex("OzelToplanmaId");

                    b.HasIndex("YasamaId");

                    b.ToTable("Birlesim", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.ClaimEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Donem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("DonemAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonemKod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DonemSecTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("EskiAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KısaAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeclisKod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MevcutUye")
                        .HasColumnType("int");

                    b.Property<int>("UyeTamsayi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Donem", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaGenelKurul", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GidenGrup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GidenGrupMu")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("GorevBasTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GorevBitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KomisyonAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<Guid>("OturumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SatırNo")
                        .HasColumnType("int");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.Property<int>("StenoIzinTuru")
                        .HasColumnType("int");

                    b.Property<decimal>("StenoSure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SureAsmaVar")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("StenografId");

                    b.ToTable("GorevAtamaGenelKurul", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaKomisyon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GidenGrup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GidenGrupMu")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("GorevBasTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GorevBitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KomisyonAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<Guid>("OturumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SatırNo")
                        .HasColumnType("int");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.Property<int>("StenoIzinTuru")
                        .HasColumnType("int");

                    b.Property<decimal>("StenoSure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SureAsmaVar")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("StenografId");

                    b.ToTable("GorevAtamaKomisyon", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaKomisyonOnay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GidenGrup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GidenGrupMu")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("GorevBasTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GorevBitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KomisyonAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<Guid>("OturumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SatırNo")
                        .HasColumnType("int");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.Property<int>("StenoIzinTuru")
                        .HasColumnType("int");

                    b.Property<decimal>("StenoSure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SureAsmaVar")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("StenografId");

                    b.ToTable("GorevAtamaKomisyonOnay", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaOzelToplanma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GidenGrup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GidenGrupMu")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("GorevBasTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GorevBitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KomisyonAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<Guid>("OturumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SatırNo")
                        .HasColumnType("int");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.Property<int>("StenoIzinTuru")
                        .HasColumnType("int");

                    b.Property<decimal>("StenoSure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SureAsmaVar")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("StenografId");

                    b.ToTable("GorevAtamaOzelToplanma", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Grup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("StenoGrupTuru")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Grup", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.GrupDetay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GidenGrupPasif")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GidenGrupSaat")
                        .HasColumnType("datetime2");

                    b.Property<int>("GidenGrupSaatUygula")
                        .HasColumnType("int");

                    b.Property<DateTime>("GidenGrupTarih")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GrupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GrupId");

                    b.ToTable("GrupDetay", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Komisyon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KomisyonTipi")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<string>("Yeri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Komisyon", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Oturum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AcanSira")
                        .HasColumnType("int");

                    b.Property<int>("AcanSiraUzman")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("KapaliOturum")
                        .HasColumnType("bit");

                    b.Property<int>("KapatanSira")
                        .HasColumnType("int");

                    b.Property<int>("KapatanSiraUzman")
                        .HasColumnType("int");

                    b.Property<string>("KatipUye_1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KatipUye_2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OturumBaskan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OturumNo")
                        .HasColumnType("int");

                    b.Property<bool>("YoklamaliMi")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.ToTable("Oturum", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.OzelGorevTur", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("OzelGorevTur", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.OzelToplanma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OzelGorevTurId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<string>("Yeri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OzelGorevTurId");

                    b.ToTable("OzelToplanma", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleClaimEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoleEntity");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Stenograf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdSoyad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BirlesimSıraNo")
                        .HasColumnType("int");

                    b.Property<string>("GidenGrupSaat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GrupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("SiraNo")
                        .HasColumnType("int");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GrupId");

                    b.HasIndex("UserId");

                    b.ToTable("Stenograf", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenografBeklemeSure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GorevOnceBeklemeSuresi")
                        .HasColumnType("int");

                    b.Property<int>("GorevSonraBeklemeSuresi")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ToplanmaTuru")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StenografBeklemeSure", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoIzin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("IzinTuru")
                        .HasColumnType("int");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StenografId");

                    b.ToTable("StenoIzin", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoToplamGenelSure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BirlesimAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Sure")
                        .HasColumnType("float");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToplantiTur")
                        .HasColumnType("int");

                    b.Property<Guid>("YasamaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("BirlesimId"), false);

                    b.HasIndex("StenografId");

                    b.ToTable("StenoToplamGenelSure", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleEntity");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Yasama", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DonemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("YasamaYili")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonemId");

                    b.ToTable("Yasama", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.AltKomisyon", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Komisyon", "Komisyon")
                        .WithMany("AltKomisyons")
                        .HasForeignKey("KomisyonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komisyon");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Birlesim", b =>
                {
                    b.HasOne("TTBS.Core.Entities.AltKomisyon", "AltKomisyon")
                        .WithMany()
                        .HasForeignKey("AltKomisyonId");

                    b.HasOne("TTBS.Core.Entities.Komisyon", "Komisyon")
                        .WithMany()
                        .HasForeignKey("KomisyonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.OzelToplanma", "OzelToplanma")
                        .WithMany()
                        .HasForeignKey("OzelToplanmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Yasama", "Yasama")
                        .WithMany("Birlesims")
                        .HasForeignKey("YasamaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AltKomisyon");

                    b.Navigation("Komisyon");

                    b.Navigation("OzelToplanma");

                    b.Navigation("Yasama");
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaGenelKurul", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany()
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany()
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaKomisyon", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany()
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany()
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaKomisyonOnay", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany()
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany()
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtamaOzelToplanma", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany()
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany()
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.GrupDetay", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Grup", "Grup")
                        .WithMany("GrupDetays")
                        .HasForeignKey("GrupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grup");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Oturum", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany("Oturums")
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");
                });

            modelBuilder.Entity("TTBS.Core.Entities.OzelToplanma", b =>
                {
                    b.HasOne("TTBS.Core.Entities.OzelGorevTur", "OzelGorevTur")
                        .WithMany()
                        .HasForeignKey("OzelGorevTurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OzelGorevTur");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleClaimEntity", b =>
                {
                    b.HasOne("TTBS.Core.Entities.ClaimEntity", "Claim")
                        .WithMany("RoleClaims")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.RoleEntity", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Stenograf", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Grup", "Grup")
                        .WithMany("Stenografs")
                        .HasForeignKey("GrupId");

                    b.HasOne("TTBS.Core.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoIzin", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany("StenoIzins")
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoToplamGenelSure", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany()
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("TTBS.Core.Entities.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Yasama", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Donem", "Donem")
                        .WithMany("Yasamas")
                        .HasForeignKey("DonemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donem");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Birlesim", b =>
                {
                    b.Navigation("Oturums");
                });

            modelBuilder.Entity("TTBS.Core.Entities.ClaimEntity", b =>
                {
                    b.Navigation("RoleClaims");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Donem", b =>
                {
                    b.Navigation("Yasamas");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Grup", b =>
                {
                    b.Navigation("GrupDetays");

                    b.Navigation("Stenografs");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Komisyon", b =>
                {
                    b.Navigation("AltKomisyons");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleEntity", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Stenograf", b =>
                {
                    b.Navigation("StenoIzins");
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserEntity", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Yasama", b =>
                {
                    b.Navigation("Birlesims");
                });
#pragma warning restore 612, 618
        }
    }
}
