using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotNetCore.Controllers.Rest;
using DotNetCore.Infrastructure.DataAccessLayer;
using DotNetCore.Infrastructure.Models;
using DotNetCore.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DotNetCore.Controllers
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

                Type controllerType = typeof(RepositoryController<,,>).MakeGenericType(new Type[] {typeof(IContext), current.AsType(), keyType});                     
                Type apiControllerType = typeof(DynamicApiController<,,>).MakeGenericType(new Type[] {typeof(IContext), current.AsType(), keyType});
                
                feature.Controllers.Add(controllerType.GetTypeInfo());
                feature.Controllers.Add(apiControllerType.GetTypeInfo());
            } 
        }
    }
}