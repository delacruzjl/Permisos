using System;
using System.Linq;
using System.Linq.Expressions;

namespace Permisos.Data.Interfaces {
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> Get();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
    }
}
