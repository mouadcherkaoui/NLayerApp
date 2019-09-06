using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NLayerApp.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace NLayerApp.Views
{
    public class EntityListViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Type entityType, dynamic modelInstance)
        {         
            var viewModel = new DynamicEntityComponentViewModel(modelInstance)
                { 
                    ModelType = typeof(IEnumerable<>).MakeGenericType(entityType), 
                    ModelInstance = modelInstance, 
                    Properties = GetPropertiesWithEditorsTypes(entityType)
                };
            //ViewBag.Model = viewModel;
            //var modelInstance = viewModel.ModelInstance;
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