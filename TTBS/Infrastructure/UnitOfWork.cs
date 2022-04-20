using Microsoft.EntityFrameworkCore;
using TTBS.Core.Interfaces;

namespace TTBS.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed;
        private Dictionary<Type, object> _repositories { get; set; }
        private readonly TTBSContext _context;
        public DbContext Context { get => _context; set { } }

        public UnitOfWork(TTBSContext context) => _context = context;

        public bool Complete() => Convert.ToBoolean(_context.SaveChanges());

        //public int ExecuteSqlCommand(string sql, params object[] param) => _context.Database.ExecuteSqlCommand(sql, param);

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new EFRepository<TEntity>((TTBSContext)Context);

            return (IRepository<TEntity>)_repositories[type];
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    if (_repositories != null)
                    {
                        _repositories.Clear();
                    }
                    // dispose the db context.
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] param) where TEntity : class
            => _context.Set<TEntity>().FromSqlRaw(sql, param);

        public int ExecuteSqlCommand(string sql, params object[] param)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
