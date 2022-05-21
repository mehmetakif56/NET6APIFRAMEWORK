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

        private void ConfigureStenoPlan(EntityTypeBuilder<StenoPlan> builder)
        {
            builder.ToTable("StenoPlan");
        }

        private void ConfigureStenoGorev(EntityTypeBuilder<StenoGorev> builder)
        {
            builder.ToTable("StenoGorev");
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
    }
}
