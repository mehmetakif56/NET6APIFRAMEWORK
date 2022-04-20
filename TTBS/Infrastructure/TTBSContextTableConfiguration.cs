using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTBS.Core.Entities;

namespace TTBS.Infrastructure
{
    public partial class TTBSContext
    {
        private void ConfigureDonem(EntityTypeBuilder<DonemEntity> builder)
        {
            builder.ToTable("Donem");
        }
    }
}
