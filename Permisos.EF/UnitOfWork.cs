using Permisos.Data;
using Permisos.Data.Interfaces;
using Permisos.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace Permisos.EF {
    public class UnitOfWork : IUnitOfWork {
        private readonly PermisosDb _dbContext;
        private bool _disposing;

        public UnitOfWork(PermisosDb dbContext) {
            _dbContext = dbContext;
        }

        public IRepository<TipoPermiso> TipoPermisos =>
            MakeRepository<TipoPermiso>();

        public IRepository<Permiso> Permisos =>
            MakeRepository<Permiso>();

        public async Task<bool> CommitAsync() {
            var results = await _dbContext.SaveChangesAsync();
            return results > 0;
        }

        public void Dispose() {
            Dispose(!_disposing);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposing) {
                return;
            }

            _disposing = true;
            GC.SuppressFinalize(this);
            _dbContext.Dispose();
        }

        private IRepository<T> MakeRepository<T>() where T : class {
            return new RepositoryBase<T>(_dbContext);
        }
    }
}
