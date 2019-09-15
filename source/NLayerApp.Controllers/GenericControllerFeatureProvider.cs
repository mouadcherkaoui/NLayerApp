using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLayerApp.Controllers.Rest;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace NLayerApp.Controllers
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            Assembly assembly = typeof(BaseEntity).GetTypeInfo().Assembly;

            IEnumerable<TypeInfo> types = assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IEntity)) && t.IsClass && !t.IsAbstract).ToList();

            foreach (var current in types)
            {
                Type keyType = current.GetProperties().FirstOrDefault(p => p.Name.Contains("Id")).PropertyType;

                // Type controllerType = typeof(RepositoryController<,,>).MakeGenericType(new Type[] {typeof(IContext), current.AsType(), keyType});                     
                Type apiControllerType = typeof(ApiCommandController<,>).MakeGenericType(new Type[] {current.AsType(), keyType});
                
                // feature.Controllers.Add(controllerType.GetTypeInfo());
                feature.Controllers.Add(apiControllerType.GetTypeInfo());
            } 
        }
    }
}