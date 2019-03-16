using NLayerAppp.Controllers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace NLayerAppp.Controllers
{
    public class GeneratedControllerNameConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if(controller.ControllerType.IsGenericType 
                && controller.ControllerType.IsSubclassOf(typeof(ApiRepositoryController<,,>))) 
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();
                if(customNameAttribute?.Route != null) {
                    controller.Selectors.Add(
                        new SelectorModel {
                            AttributeRouteModel = 
                                new AttributeRouteModel(new RouteAttribute(customNameAttribute.Route))
                        }
                    );
                }
            }
        }
    }
}