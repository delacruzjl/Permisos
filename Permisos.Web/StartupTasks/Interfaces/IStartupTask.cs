using System.Threading;
using System.Threading.Tasks;

namespace Permisos.Web.StartupTasks.Interfaces {
    public interface IStartupTask {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
