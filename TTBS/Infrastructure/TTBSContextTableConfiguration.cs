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
        private void ConfigureOzelGorev(EntityTypeBuilder<OzelToplanma> builder)
        {
            builder.ToTable("OzelGorev");
        }
        private void ConfigureOturum(EntityTypeBuilder<Oturum> builder)
        {
            builder.ToTable("Oturum");
        }

        private void ConfigureStenoGrup(EntityTypeBuilder<StenoGrup> builder)
        {
            builder.ToTable("StenoGrup");

            builder.Ignore("Id");

            builder.HasKey(ur => new { ur.StenoId, ur.GrupId });

            builder.HasOne(ur => ur.Stenograf)
                            .WithMany(r => r.StenoGrups)
                            .HasForeignKey(ur => ur.StenoId)
                            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Grup)
                            .WithMany(u => u.StenoGrups)
                            .HasForeignKey(ur => ur.GrupId)
                            .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureStenoToplamGenelSure(EntityTypeBuilder<StenoToplamGenelSure> builder)
        {
            builder.ToTable("StenoToplamGenelSure");
        }
    }
}
