using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLayerApp.Blazor.ServerApp.Data;
using NLayerApp.Controllers;
using NLayerApp.DataAccessLayer;
using NLayerApp.DataAccessLayer.Handlers;
using NLayerApp.DataAccessLayer.Requests;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Models;

namespace NLayer.Blazor.ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var defaultConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var navManager = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(navManager.BaseUri) //new Uri("http://localhost:*")
                    };
                });
            }
            var types =
            new Type[] {
                typeof(Member), typeof(Group), typeof(GroupMembers),
                typeof(Subject), typeof(Room), typeof(Page)};


            foreach (var type in types)
            {
                // ReadEntities Handler
                var handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(ReadEntitiesRequest<>).MakeGenericType(type), typeof(IEnumerable<>).MakeGenericType(type) });
                services.AddScoped(handlerType, typeof(ReadEntitiesRequestHandler<>).MakeGenericType(type));

                // ReadEntity(id) Handler
                handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(ReadEntityRequest<,>).MakeGenericType(type, typeof(int)), type });
                services.AddScoped(handlerType, typeof(ReadEntityRequestHandler<,>).MakeGenericType(type, typeof(int)));

                // CreateEntity Handler
                handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(CreateEntityRequest<>).MakeGenericType(type), type });
                services.AddScoped(handlerType, typeof(CreateEntityRequestHandler<>).MakeGenericType(type));

                // UpdateEntity({id,entity}) Handler
                handlerType = typeof(IRequestHandler<,>)
                    .MakeGenericType(new Type[] { typeof(UpdateEntityRequest<,>).MakeGenericType(type, typeof(int)), type });
                services.AddScoped(handlerType, typeof(UpdateEntityRequestHandler<,>).MakeGenericType(type, typeof(int)));

                // Adding GenericApiEndpointService<TType>
                var endpointServiceType = typeof(GenericApiEndpointService<>).MakeGenericType(type);
                services.AddScoped(endpointServiceType);

            }

            services.AddMediatR(new Type[] { typeof(ReadEntitiesRequestHandler<>) });

            services.AddScoped<IContext>((s) => new AppDbContext(defaultConnectionString, types));

            services
                .AddMvc(options => {
                    options.EnableEndpointRouting = false;
                })
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.MaxDepth = 0;
                    // Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .UseDynamicControllers()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "/api/{controller}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                /*endpoints.MapDynamicControllerRoute*/
                endpoints.MapControllers();
            });            

        }
    }

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder UseDynamicControllers(this IMvcBuilder builder)
        {
            return builder.ConfigureApplicationPartManager(
                    partManager =>
                    {
                        partManager.FeatureProviders.Add(new GenericControllerFeatureProvider());
                    });
        }
    }
}
