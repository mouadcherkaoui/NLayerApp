using System;
using DotNetCore.Controllers.Rest;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DotNetCore.Controllers.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public class GenericControllerNameConvention : Attribute, IControllerModelConvention//, IActionModelConvention
    {
        private Type _controllerType;
        public GenericControllerNameConvention(Type controllerType)
        {
            _controllerType = controllerType;
        }
        public void Apply(ControllerModel controller)
        {
            if(controller.ControllerType.GetGenericTypeDefinition() == typeof(RepositoryController<,,>))
            {
                var entityType = controller.ControllerType.GenericTypeArguments[1];
                controller.ControllerName = $"{entityType.Name}s";                
            } 
            else if(controller.ControllerType.GetGenericTypeDefinition() == typeof(DynamicApiController<,,>))
            {
                var entityType = controller.ControllerType.GenericTypeArguments[1];
                controller.ControllerName = $"{entityType.Name}s";
            }
        }
    }
}