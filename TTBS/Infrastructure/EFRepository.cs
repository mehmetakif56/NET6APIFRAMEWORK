using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TTBS.Core.Interfaces;

namespace TTBS.Infrastructure
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly TTBSContext _dbContext;
        private DbSet<T> _dbSet { get; set; }

        public EFRepository(TTBSContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Create(T entity, Guid? userId = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = userId;
            _dbSet.Add(entity);
        }

        public void Create(Guid? userId = null, params T[] entities)
        {
            entities.ToList().ForEach((e) =>
            {
                e.CreatedDate = DateTime.Now;
                e.CreatedBy = userId;
            });

            _dbSet.AddRange(entities);
        }

        public void Create(IEnumerable<T> entities, Guid? userId = null)
        {
            entities.ToList().ForEach((e) =>
            {
                e.CreatedDate = DateTime.Now;
                e.CreatedBy = userId;
            });
            _dbSet.AddRange(entities);
        }

        public void Delete(params T[] entities) => _dbSet.RemoveRange(entities);

        public void Delete(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public virtual void Delete(object id)
        {
            T entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Update(T entity, Guid? userId = null)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = userId;
            _dbSet.Update(entity);
        }

        public void Update(Guid? userId = null, params T[] entities)
        {
            entities.ToList().ForEach((e) =>
            {
                e.ModifiedDate = DateTime.Now;
                e.ModifiedBy = userId;
            });
            _dbSet.UpdateRange(entities);
        }

        public void Update(IEnumerable<T> entities, Guid? userId = null, bool bulkInsert = false)
        {
            entities.ToList().ForEach((e) =>
            {
                e.ModifiedDate = DateTime.Now;
                e.ModifiedBy = userId;
            });
            if (bulkInsert)
                _dbSet.UpdateRange(entities);
            //_dbContext.BulkUpdate(entities);
            else
                _dbSet.UpdateRange(entities);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool asNoTracking = false, int? skip = null, int? take = null)
            => GetQueryable(filter, orderBy, includeProperties, asNoTracking, skip, take);

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool asNoTracking = false, int? skip = null, int? take = null)
            => GetQueryable(null, orderBy, includeProperties, asNoTracking, skip, take);

        public T GetById(object id) => _dbSet.Find(id);

        public T GetFirst(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
            => GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();

        public T GetOne(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool asNoTracking = false)
            => GetQueryable(filter, null, includeProperties, asNoTracking).SingleOrDefault();

        protected virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    string includeProperties = null,
                                                    bool asNoTracking = false,
                                                    int? skip = null,
                                                    int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }


            return query;
        }

        public bool Save()
        {
            try
            {
                return Convert.ToBoolean(_dbContext.SaveChanges());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> FromSql(string query, params object[] param)
        {
            return _dbSet.FromSqlRaw<T>(query, param).AsNoTracking();
        }
    }
}
