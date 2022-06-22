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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("KomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OzelToplanmaId")
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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UyeTamsayi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Donem", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtama", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("GorevBasTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GorevBitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("GorevDakika")
                        .HasColumnType("int");

                    b.Property<int>("GorevSaniye")
                        .HasColumnType("int");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OturumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("OturumId");

                    b.HasIndex("StenografId");

                    b.ToTable("GorevAtama", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Grup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Grup", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Komisyon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KomisyonTipi")
                        .HasColumnType("int");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("KapaliOturum")
                        .HasColumnType("bit");

                    b.Property<string>("KatipUye_1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KatipUye_2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OzelGorevTurId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<string>("Yeri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OzelGorevTurId");

                    b.ToTable("OzelGorev", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleClaimEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SiraNo")
                        .HasColumnType("int");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stenograf", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenografBeklemeSure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GorevOnceBeklemeSuresi")
                        .HasColumnType("int");

                    b.Property<int>("GorevSonraBeklemeSuresi")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlanTuru")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StenografBeklemeSure", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoGrup", b =>
                {
                    b.Property<Guid>("StenoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GrupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("StenoId", "GrupId");

                    b.HasIndex("GrupId");

                    b.ToTable("StenoGrup", (string)null);
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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("IzinTuru")
                        .HasColumnType("int");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StenografId");

                    b.ToTable("StenoIzin", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DonemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

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
                    b.HasOne("TTBS.Core.Entities.Yasama", "Yasama")
                        .WithMany("Birlesims")
                        .HasForeignKey("YasamaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Yasama");
                });

            modelBuilder.Entity("TTBS.Core.Entities.GorevAtama", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany()
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Oturum", "Oturum")
                        .WithMany()
                        .HasForeignKey("OturumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany("GorevAtamas")
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");

                    b.Navigation("Oturum");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Oturum", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany()
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

            modelBuilder.Entity("TTBS.Core.Entities.StenoGrup", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Grup", "Grup")
                        .WithMany("StenoGrups")
                        .HasForeignKey("GrupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany("StenoGrups")
                        .HasForeignKey("StenoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grup");

                    b.Navigation("Stenograf");
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
                    b.Navigation("StenoGrups");
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
                    b.Navigation("GorevAtamas");

                    b.Navigation("StenoGrups");

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
