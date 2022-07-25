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
        public DbSet<Birlesim> Birlesims { get; set; }
        public DbSet<Komisyon> Komisyons { get; set; }
        public DbSet<GorevAtama> GorevAtamas { get; set; }
        public DbSet<StenoIzin> StenoIzin { get; set; }
        public DbSet<Stenograf> Stenografs { get; set; }
        public DbSet<StenoGrup> StenoGrups { get; set; }
        public DbSet<Grup> Grups { get; set; }
        public DbSet<ClaimEntity> Claims { get; set; }
        public DbSet<RoleClaimEntity> RoleClaims { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<StenografBeklemeSure> StenografBeklemeSures { get; set; }
        public DbSet<OzelGorevTur> OzelGorevTurs { get; set; }
        public DbSet<OzelToplanma> OzelGorevs { get; set; }
        public DbSet<Oturum> Oturums { get; set; }
        public DbSet<StenoToplamGenelSure> stenoToplamGenelSures { get; set; }



        public DbSet<AltKomisyon> AltKomisyons { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyBaseEntityConfiguration();
            builder.Entity<Birlesim>(ConfigureBirlesim);
            builder.Entity<Komisyon>(ConfigureKomisyon);
            builder.Entity<Donem>(ConfigureDonem);
            builder.Entity<StenoIzin>(ConfigureStenoIzin);
            builder.Entity<GorevAtama>(ConfigureStenoGorev);
            builder.Entity<Yasama>(ConfigureYasama);
            builder.Entity<Stenograf>(ConfigureStenograf);
            builder.Entity<Grup>(ConfigureGrup);
            builder.Entity<StenoGrup>(ConfigureStenoGrup);
            builder.Entity<AltKomisyon>(ConfigurealtKom);
            builder.Entity<StenografBeklemeSure>(ConfigureBeklemeSure);
            builder.Entity<OzelGorevTur>(ConfigureOzelGorevTur);
            builder.Entity<OzelToplanma>(ConfigureOzelGorev);
            builder.Entity<Oturum>(ConfigureOturum);
<<<<<<< HEAD
            builder.Entity<StenoToplamGenelSure>(ConfigureStenoToplamGenelSure);
=======
           
>>>>>>> e8454eeafc1c7518024ba997177bd1e562595dc4
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory)  //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();
        }
    }
}
