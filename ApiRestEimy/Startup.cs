using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApiRestEimy.DB;
using ApiRestEimy.Repositorio;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiRestEimy.Helper;
using ApiRestEimy.Interfaces;
using Serilog;
using ApiRestEimy.Services;

namespace ApiRestEimy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string name = String.Format(".\\log_{0}.txt", DateTime.Now.ToString("dd-MM-yyyy_HH_mm"));
            Log.Logger = new LoggerConfiguration()//.ReadFrom.Configuration(configuration).CreateLogger();
                .MinimumLevel.Debug()
                .WriteTo.File(name)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IPerfilesPersonasRepo, PerfilesPersonasRepo>();
            services.AddScoped<IUsuarioRepo, UsuarioRepo>();
            services.AddDbContext<PerfilPersonasContext>(o => o.UseSqlite("Data source=PerfilesPersonas.db"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiRestEimy", Version = "v1" });
            });
            services.AddScoped<IUserService, UserService>();
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiRestEimy v1"));
            }

            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
