using Microsoft.Extensions.DependencyInjection;
using Permisos.Web.StartupTasks.Interfaces;

namespace Permisos.Web.StartupTasks {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
        where T : class, IStartupTask
        => services.AddTransient<IStartupTask, T>();
    }
}
