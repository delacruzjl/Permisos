using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Permisos.Web.StartupTasks.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Permisos.Web.StartupTasks {
    public static class StartupTaskWebHostExtensions {
        public static async Task RunWithStartupTasksAsync(this IHost webHost, CancellationToken cancellationToken = default) {
            var startupTasks = webHost.Services.GetServices<IStartupTask>();
            foreach (var startupTask in startupTasks) {
                await startupTask.ExecuteAsync(cancellationToken);
            }

            await webHost.RunAsync(cancellationToken);
        }
    }
}
