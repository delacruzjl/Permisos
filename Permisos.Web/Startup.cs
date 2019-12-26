using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Permisos.Data.Interfaces;
using Permisos.EF;
using Permisos.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Permisos.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => options.AddPolicy("defaultPolicy", builder => {
                builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddDbContext<PermisosDb>(opt => opt.UseInMemoryDatabase(nameof(PermisosDb)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/dist";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment()) {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseCors("defaultPolicy");
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });


            SeedTipoPermisos(app);
        }

        private static void SeedTipoPermisos(IApplicationBuilder app) {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var uow = serviceScope.ServiceProvider.GetService<IUnitOfWork>();

            new List<string> {
                "Enfermedad", "Diligencia"
            }.ForEach(tipo => uow.TipoPermisos.Add(
                new Data.TipoPermiso {
                    Descripcion = tipo
            }));

            uow.Commit();
        }
    }
}
