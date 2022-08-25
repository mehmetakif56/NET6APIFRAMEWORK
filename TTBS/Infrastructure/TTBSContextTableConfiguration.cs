using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTBS.Core.Entities;

namespace TTBS.Infrastructure
{
    public partial class TTBSContext
    {
        private void ConfigureDonem(EntityTypeBuilder<Donem> builder)
        {
            builder.ToTable("Donem");
        }

        private void ConfigureYasama(EntityTypeBuilder<Yasama> builder)
        {
            builder.ToTable("Yasama");
        }

        private void ConfigureBirlesim(EntityTypeBuilder<Birlesim> builder)
        {
            builder.ToTable("Birlesim");
        }

        private void ConfigureKomisyon(EntityTypeBuilder<Komisyon> builder)
        {
            builder.ToTable("Komisyon");
        }

        private void ConfigureStenoGorev(EntityTypeBuilder<GorevAtama> builder)
        {
            builder.ToTable("GorevAtama");
        }

        private void ConfigureStenoIzin(EntityTypeBuilder<StenoIzin> builder)
        {
            builder.ToTable("StenoIzin");
        }

        private void ConfigureStenograf(EntityTypeBuilder<Stenograf> builder)
        {
            builder.ToTable("Stenograf");
        }

        private void ConfigureGrup(EntityTypeBuilder<Grup> builder)
        {
            builder.ToTable("Grup");
        }
        private void ConfigurealtKom(EntityTypeBuilder<AltKomisyon> builder)
        {
            builder.ToTable("AltKomisyon");
        }

        private void ConfigureBeklemeSure(EntityTypeBuilder<StenografBeklemeSure> builder)
        {
            builder.ToTable("StenografBeklemeSure");
        }
        private void ConfigureOzelGorevTur(EntityTypeBuilder<OzelGorevTur> builder)
        {
            builder.ToTable("OzelGorevTur");
        }
        private void ConfigureOzelToplanma(EntityTypeBuilder<OzelToplanma> builder)
        {
            builder.ToTable("OzelToplanma");
        }
        private void ConfigureOturum(EntityTypeBuilder<Oturum> builder)
        {
            builder.ToTable("Oturum");
        }

        private void ConfigureGrupDetay(EntityTypeBuilder<GrupDetay> builder)
        {
            builder.ToTable("GrupDetay");
        }

        private void ConfigureBirlesimOzelToplanma(EntityTypeBuilder<BirlesimOzelToplanma> builder)
        {
            builder.ToTable("BirlesimOzelToplanma");

            builder.Ignore("Id");

            builder.HasKey(ur => new { ur.BirlesimId, ur.OzelToplanmaId });

            builder.HasOne(ur => ur.Birlesim)
                            .WithMany(r => r.BirlesimOzelToplanmas)
                            .HasForeignKey(ur => ur.BirlesimId)
                            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.OzelToplanma)
                            .WithMany(u => u.BirlesimOzelToplanmas)
                            .HasForeignKey(ur => ur.OzelToplanmaId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
        private void ConfigureBirlesimKomisyon(EntityTypeBuilder<BirlesimKomisyon> builder)
        {
            builder.ToTable("BirlesimKomisyon");

            builder.Ignore("Id");

            builder.HasKey(ur => new { ur.BirlesimId, ur.KomisyonId });

            builder.HasOne(ur => ur.Birlesim)
                            .WithMany(r => r.BirlesimKomisyons)
                            .HasForeignKey(ur => ur.BirlesimId)
                            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Komisyon)
                            .WithMany(u => u.BirlesimKomisyons)
                            .HasForeignKey(ur => ur.KomisyonId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
        private void ConfigureStenoToplamGenelSure(EntityTypeBuilder<StenoToplamGenelSure> builder)
        {
            builder.ToTable("StenoToplamGenelSure");
            builder.HasIndex(e => e.BirlesimId).IsClustered(false);
        }
        private void ConfigureGorevAtamaKomisyon(EntityTypeBuilder<GorevAtamaKomisyon> builder)
        {
            builder.ToTable("GorevAtamaKomisyon");
        }
        private void ConfigureGorevAtamaGenelKurul(EntityTypeBuilder<GorevAtamaGenelKurul> builder)
        {
            builder.ToTable("GorevAtamaGenelKurul");
        }
        private void ConfigureGorevAtamaOzelToplanma(EntityTypeBuilder<GorevAtamaOzelToplanma> builder)
        {
            builder.ToTable("GorevAtamaOzelToplanma");
        }
        private void ConfigureGorevAtamalar(EntityTypeBuilder<GorevAtamalar> builder)
        {
            builder
            .ToView(nameof(GorevAtamalar))
            .HasKey(t => t.BirlesimId);
        }
    }
}
