using Microsoft.EntityFrameworkCore;
using TTBS.Core.Entities;

namespace TTBS.Infrastructure
{
    public partial class TTBSContext : DbContext
    {
        public static readonly LoggerFactory _myLoggerFactory = new LoggerFactory(new[] {
                                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
                        });
        public TTBSContext() { }
        public TTBSContext(DbContextOptions<TTBSContext> options) : base(options)
        {
          
        }

        public DbSet<Donem> Donems { get; set; }
        public DbSet<Yasama> Yasamas { get; set; }
        public DbSet<StenoPlan> StenoPlans { get; set; }
        public DbSet<StenoGorev> StenoGorevs { get; set; }
        public DbSet<StenoIzin> StenoIzin { get; set; }
        public DbSet<Stenograf> Stenografs { get; set; }
        public DbSet<StenoGrup> StenoGrups { get; set; }
        public DbSet<Grup> Grups { get; set; }
        public DbSet<ClaimEntity> Claims { get; set; }
        public DbSet<RoleClaimEntity> RoleClaims { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyBaseEntityConfiguration();

            builder.Entity<Donem>(ConfigureDonem);
            builder.Entity<StenoPlan>(ConfigureStenoPlan);
            builder.Entity<StenoIzin>(ConfigureStenoIzin);
            builder.Entity<StenoGorev>(ConfigureStenoGorev);
            builder.Entity<Yasama>(ConfigureYasama);
            builder.Entity<Stenograf>(ConfigureStenograf);
            builder.Entity<Grup>(ConfigureGrup);
            builder.Entity<StenoGrup>(ConfigureStenoGrup);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory)  //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();
        }
    }
}
