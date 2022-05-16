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

       
    }
}
