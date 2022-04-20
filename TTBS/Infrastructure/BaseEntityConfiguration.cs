using System.Reflection;
using TTBS.Core.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace TTBS.Infrastructure
{
    public static class BaseEntityConfiguration
    {
        static void Configure<TEntity, T>(ModelBuilder modelBuilder)
            where TEntity : BaseEntity<T>
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                // Id value will be generated
                builder.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }

        static void ConfigureQueryFilter<TEntity, T>(ModelBuilder modelBuilder) where TEntity : BaseEntity<T>
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder)
        {
            var configureMethod = typeof(BaseEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(Configure));
            var queryFilterMethod = typeof(BaseEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureQueryFilter));
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsBaseEntity(out var T))
                {
                    configureMethod.MakeGenericMethod(entityType.ClrType, T).Invoke(null, new[] { modelBuilder });
                    queryFilterMethod.MakeGenericMethod(entityType.ClrType, T).Invoke(null, new[] { modelBuilder });
                }

            }
            return modelBuilder;
        }

        static bool IsBaseEntity(this Type type, out Type T)
        {
            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {
                    T = baseType.GetGenericArguments()[0];
                    return true;
                }
            }
            T = null;
            return false;
        }
    }
}
