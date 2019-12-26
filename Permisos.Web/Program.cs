using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Permisos.Web.StartupTasks;

namespace Permisos.Web {
    public class Program {
        public static async Task Main(string[] args) {
            await CreateHostBuilder(args).Build().RunWithStartupTasksAsync();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
