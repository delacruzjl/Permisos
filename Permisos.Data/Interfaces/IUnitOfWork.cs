using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permisos.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TipoPermiso> TipoPermisos { get; }
        IRepository<Permiso> Permisos { get; }
        Task<bool> CommitAsync();
    }
}
