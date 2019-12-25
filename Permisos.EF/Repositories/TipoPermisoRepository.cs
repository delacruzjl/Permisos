using Permisos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permisos.EF.Repositories
{
    public class TipoPermisoRepository : RepositoryBase<TipoPermiso> {
        public TipoPermisoRepository(PermisosDb dbContext) : base(dbContext) {
        }
    }
}
