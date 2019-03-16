using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLayerdApp.Controllers.Attributes;
using NLayerdApp.DataAccessLayer;
using NLayerdApp.Infrastructure.DataAccessLayer;
using NLayerdApp.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace NLayerdApp.Controllers
{
    public class GeneratedControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentAssembly = typeof(BaseEntity).Assembly;
            var candidates = currentAssembly
                .DefinedTypes
                .Where(x => x.GetCustomAttributes().Any(a => a.GetType() == typeof(GeneratedControllerAttribute))).ToList();//.Any(a => a.GetType() == typeof(GeneratedControllerAttribute))).ToList();
            foreach (var current in candidates)
            {
                var idType = current.GetProperty("Id").PropertyType;
                feature.Controllers.Add(typeof(ApiRepositoryController<,,>).MakeGenericType(new Type[] {typeof(AppDataContext), current, idType}).GetTypeInfo());
            }
        }
    }
}