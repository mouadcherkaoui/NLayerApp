using System;
using System.Linq;
using System.Reflection;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace NLayerApp.Repositories
{
    public static class ServiceCollectionRepositoriesExtensions
    {
        public static void RegisterTypedRepositories(this IServiceCollection services, Type[] types)
        {
            foreach (var current in types)
            {
                var keyType = current.GetProperties().FirstOrDefault(p => p.Name.Contains("Id")).PropertyType;
                var serviceType = typeof(IRepository<,>).MakeGenericType(new Type[]{current, keyType});
                var implementationType = typeof(Repository<,,>).MakeGenericType(new Type[]{ typeof(IContext), current, keyType});

                services.AddTransient(serviceType, implementationType);
            }

            //return services;
        }
    }
}