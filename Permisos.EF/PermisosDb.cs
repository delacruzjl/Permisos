using Microsoft.EntityFrameworkCore;
using Permisos.Data;

namespace Permisos.EF {
    public class PermisosDb : DbContext {
        public PermisosDb(DbContextOptions<PermisosDb> options) : base(options) {

        }

        public DbSet<TipoPermiso> TipoPermisos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }

    }
}
