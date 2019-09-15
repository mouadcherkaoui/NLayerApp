using System;
using NLayerApp.Controllers.Rest;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NLayerApp.Controllers.Attributes
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
            if(controller.ControllerType.IsGenericType && 
                controller.ControllerType.GetGenericTypeDefinition() == typeof(ApiCommandController<,>))
            {
                var entityType = controller.ControllerType.GenericTypeArguments[0];
                controller.ControllerName = $"{entityType.Name}s";
            }
        }
    }
}