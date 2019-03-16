using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerAppp.Repositories;
using System;
using NLayerAppp.Models;
using NLayerAppp.Controllers;
using NLayerAppp.DataAccessLayer;
using NLayerAppp.Infrastructure.DataAccessLayer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Annotations;

namespace NLayerAppp.MvcApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IContext, AppDataContext>();
            // services.
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { 
                    Title = "Dynamic API", 
                    Version = "v1", 
                    Description = "A simple example ASP.NET Core Dynamic Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Mouad Cherkaoui",
                        Email = string.Empty,
                        Url = "https://twitter.com/mouadcherkaoui"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                c.EnableAnnotations();
            });


            services.RegisterTypedRepositories(new Type[]{typeof(Member), typeof(Group), typeof(Subject), typeof(Room)});
            services
                .AddMvc(o => o.Conventions.Add(new GeneratedControllerNameConvention()))
                .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GeneratedControllerFeatureProvider()))
                .UseDynamicControllers()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }

        public static class MvcBuilderExtensions
    {
        public static IMvcBuilder UseDynamicControllers(this IMvcBuilder builder)
        {
            return  builder.ConfigureApplicationPartManager(
                    partManager =>
                    {
                        partManager.FeatureProviders.Add(new GenericControllerFeatureProvider());
                    });
        }
    }
}
