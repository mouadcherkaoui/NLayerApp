using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NLayerAppp.Views.ViewModels;

namespace NLayerAppp.Views
{
    public class EntityViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Type entityType, object modelInstance = null, string viewType = "")
        {         
            var viewModel = new DynamicEntityComponentViewModel(modelInstance)
                { 
                    ViewType = viewType 
                };

            return await Task.FromResult(View(viewModel));
        }

        private Dictionary<PropertyInfo, string> GetPropertiesWithEditorsTypes(Type entityType)
        {
            var propertiesDictionary = new Dictionary<PropertyInfo, string>();
            var properties = entityType.GetTypeInfo().DeclaredProperties.ToList();
            foreach (var current in properties)
            {
                string editorType = "";
                switch (current.PropertyType.Name)
                {
                    case nameof(String):
                        editorType = "text";
                        break;
                    case nameof(Int32):
                        editorType = "number";
                        break;
                    case nameof(Boolean):
                        editorType = "checkbox";
                        break;
                    case nameof(DateTime):
                        editorType = "date";
                        break;
                }
                editorType = 
                    current.GetCustomAttribute(typeof(EmailAddressAttribute)) != null ? "email" : editorType;

                propertiesDictionary.Add(current, editorType);
            }
            return propertiesDictionary;
        }
    }
}