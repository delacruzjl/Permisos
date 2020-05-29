using Microsoft.Extensions.DependencyInjection;
using Permisos.Data.Interfaces;
using Permisos.Web.StartupTasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Permisos.Web.StartupTasks {
    public class TipoPermisoStartup : IStartupTask {
        private readonly IServiceProvider _serviceProvider;
        private static readonly List<string> _defaultTipoPermisos = new List<string> {
                "Enfermedad", "Diligencia"
                };

        public TipoPermisoStartup(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default) {
            using var serviceScope = _serviceProvider.CreateScope();
            var uow = serviceScope.ServiceProvider.GetService<IUnitOfWork>();

            if (uow.TipoPermisos.Get().Any()) {
                return;
            }

            _defaultTipoPermisos.ForEach(tipo => uow.TipoPermisos.Add(
                            new Data.TipoPermiso {
                                Descripcion = tipo
                            }));

            await uow.CommitAsync();
        }
    }
}
