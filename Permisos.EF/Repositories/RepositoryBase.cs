using Microsoft.EntityFrameworkCore;
using Permisos.Data.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Permisos.EF.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly PermisosDb _dbContext;
        private bool _disposed = false;
        protected DbSet<T> _dbSet { get; set; }

        public RepositoryBase(PermisosDb dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));

            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            var newEntry = _dbContext.Entry(entity);
            if (newEntry.State != EntityState.Detached)
            {
                newEntry.State = EntityState.Added;
                return;
            }

            _dbSet.Add(entity);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> Get()
        {
            return _dbSet;
        }

        public void Remove(T entity)
        {
            var dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
                return;
            }

            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
                _dbSet = null;
            }

            _disposed = true;
        }
    }
}
