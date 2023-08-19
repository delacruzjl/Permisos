using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Permisos.Data.Interfaces;
using Permisos.EF;
using Permisos.Web.StartupTasks;
using Permisos.Web.ViewModels;

namespace Permisos.Web
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder =
        new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddEnvironmentVariables();

      builder.AddUserSecrets<Startup>();

      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      AddCors (services);
      services
        .AddDbContext<PermisosDb>(opt =>
          opt
            .UseSqlServer(Configuration[nameof(PermisosDb)],
            b => b.MigrationsAssembly(typeof (PermisosDb).Assembly.FullName)));
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddStartupTask<DatabaseCreationStartup>();
      services.AddStartupTask<TipoPermisoStartup>();
      AddAutoMapper (services);

      services
        .AddMvc(opt => { 
            opt.EnableEndpointRouting = false;
        })
        .AddJsonOptions(opt =>
        {
          opt.JsonSerializerOptions.IgnoreNullValues = true;
        });

      services
        .AddSwaggerGen(options =>
          options
            .SwaggerDoc("v1",
            new OpenApiInfo {
              Title = "Permisos API",
              Version = "v1",
              TermsOfService = new System.Uri("http://www.delacruzhome.com/")
            }));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(
      IApplicationBuilder app,
      IWebHostEnvironment env
    )
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app
        .UseSwaggerUI(c =>
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

      app.UseRouting();
      app.UseCors("defaultPolicy");
      app.UseMvc();
    }

    //////
    private static void AddAutoMapper(IServiceCollection services)
    {
      var config =
        new MapperConfiguration(cfg =>
          {
            cfg.AddProfile(new MappingProfile());
          });

      var mapper = config.CreateMapper();
      services.AddSingleton (mapper);
    }

    private static void AddCors(IServiceCollection services)
    {
      services
        .AddCors(options =>
          options
            .AddPolicy("defaultPolicy",
            builder =>
            {
              builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
    }
  }
}
