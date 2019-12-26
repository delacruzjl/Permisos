using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Permisos.Data.Interfaces;

namespace Permisos.Web {
    public class Program {
        public static async Task Main(string[] args) {
            var webHost = CreateHostBuilder(args).Build();
            using var scope = webHost.Services.CreateScope();
            await SeedTipoPermiso(scope);
            await webHost.RunAsync();

            //////

            async Task SeedTipoPermiso(IServiceScope serviceScope) {
                var uow = serviceScope.ServiceProvider.GetService<IUnitOfWork>();

                new List<string> {
                "Enfermedad", "Diligencia"
            }.ForEach(tipo => uow.TipoPermisos.Add(
                new Data.TipoPermiso {
                    Descripcion = tipo
                }));

                await uow.CommitAsync();
            }
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
