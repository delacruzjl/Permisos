using Permisos.Data;
using Permisos.Data.Interfaces;
using Permisos.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace Permisos.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PermisosDb _dbContext;
        private bool _disposed = false;

        public UnitOfWork(PermisosDb dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TipoPermiso> TipoPermisos =>
            MakeRepository<TipoPermiso>();

        public IRepository<Permiso> Permisos =>
            MakeRepository<Permiso>();

        public async Task<bool> CommitAsync()
        {
            var results = await _dbContext.SaveChangesAsync();
            return results > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        private IRepository<T> MakeRepository<T>() where T : class
        {
            return new RepositoryBase<T>(_dbContext);
        }
    }
}
