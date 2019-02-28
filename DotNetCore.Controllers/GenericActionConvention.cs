using System;
using System.Linq;
using System.Reflection;
using DotNetCore.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DotNetCore.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenericActionConvention : Attribute, IActionModelConvention
    {
        public GenericActionConvention()
        {
            
        }

        public void Apply(ActionModel action)
        {
            foreach(var parameter in action.Parameters)
            {
                if(parameter.ParameterInfo.ParameterType.GetInterfaces().Contains(typeof(IEntity)))
                {
                    var properties = parameter.Properties;
                }
                var bindingInfo = parameter.BindingInfo;
                var parameterInfo = parameter.ParameterInfo;
            }
        }
    }
}