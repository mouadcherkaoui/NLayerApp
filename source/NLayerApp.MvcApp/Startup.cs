using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerApp.Repositories;
using System;
using NLayerApp.Models;
using NLayerApp.Controllers;
using NLayerApp.DataAccessLayer;
using NLayerApp.Infrastructure.DataAccessLayer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using NLayerApp.DataAccessLayer.Configurations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using NLayerApp.DataAccessLayer.Commands;
using NLayerApp.DataAccessLayer.Requests;
using NLayerApp.Controllers.Attributes;
using NLayerApp.DataAccessLayer.Handlers;

namespace NLayerApp.MvcApp
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
            var types =
                new Type[] {
                    typeof(Member), typeof(Group), typeof(GroupMembers),
                    typeof(Subject), typeof(Room)};


            foreach(var type in types)
            {
                // ReadEntities Handler
                var handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(ReadEntitiesRequest<>).MakeGenericType(type), typeof(IEnumerable<>).MakeGenericType(type) });
                services.AddScoped(handlerType, typeof(ReadEntitiesRequestHandler<>).MakeGenericType(type));

                // ReadEntity(id) Handler
                handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(ReadEntityRequest<,>).MakeGenericType(type, typeof(int)), type });
                services.AddScoped(handlerType, typeof(ReadEntityRequestHandler<, >).MakeGenericType(type, typeof(int)));

                // CreateEntity Handler
                handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(CreateEntityRequest<>).MakeGenericType(type), type });
                services.AddScoped(handlerType, typeof(CreateEntityRequestHandler<>).MakeGenericType(type));

                // UpdateEntity({id,entity}) Handler
                handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(UpdateEntityRequest<,>).MakeGenericType(type, typeof(int)), type });
                services.AddScoped(handlerType, typeof(UpdateEntityRequestHandler<,>).MakeGenericType(type, typeof(int)));

            }

            services.AddMediatR(new Type[] { typeof(ReadEntitiesRequestHandler<>) });

            services.AddScoped<IContext>((s) => new AppDbContext(@"Server=.\;Initial Catalog=nlayerappdb;Integrated Security=True;", types));


            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

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


            services.RegisterTypedRepositories(new Type[]{typeof(Member), typeof(Group), typeof(GroupMembers), typeof(Subject), typeof(Room)});
            services
                .AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "/api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
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
