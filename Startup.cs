using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers;
using AgilFood.Core;
using AgilFood.Core.models;
using AgilFood.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgilFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configurando as photos
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));

            //Temos que registrar aqui as nossas interfaces
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<ICardapioRepository, CardapioRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddAutoMapper();
            services.AddDbContext<AgilFoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            //Authorization, restringir o acesso a certos Actions conforme os roles, para que alem de estar autenticado, esteja autorizado
            services.AddAuthorization(options => {
                options.AddPolicy(Policies.RequireAdminRole, policy => policy.RequireClaim("https://agilfood.com/roles", "Admin"));
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var options = new JwtBearerOptions
            {
                Audience = "https://api.agilfood.com",
                Authority = "https://agilfood.auth0.com/"
            };
            app.UseJwtBearerAuthentication(options); //here is the medleware

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
