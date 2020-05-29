using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Permisos.EF;
using Permisos.Web.StartupTasks.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Permisos.Web.StartupTasks {
    public class DatabaseCreationStartup : IStartupTask {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseCreationStartup(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        public async Task ExecuteAsync(CancellationToken cancellationToken = default) {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<PermisosDb>();
            await context.Database.MigrateAsync();
        }
    }
}
