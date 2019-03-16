using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NLayerAppp.Views.ViewModels
{
    public class EntityComponentViewModel
    {
        public EntityComponentViewModel(Type modelType)
        {
            ModelType = modelType;
        }

        public EntityComponentViewModel(object modelInstance)
        {
            ModelInstance = modelInstance;   
            ModelType = modelInstance.GetType().IsConstructedGenericType 
                ? modelInstance.GetType().GetGenericArguments()[0]
                : modelInstance.GetType();
            Properties = GetPropertiesWithEditorsTypes(ModelInstance.GetType()); 
            //ModelType = modelInstance.GetType();
        }

        public object this[string index]
        {
            get{
                return ModelInstance.GetType().GetProperty(index).GetValue(ModelInstance);
            }
        }

        public Expression<Func<object, object>> GetPropertyAccessExpression(string propertyName)
        {
            return m => m.GetType().GetProperty(propertyName).GetGetMethod().Invoke(m, null);
        }
        public EntityComponentViewModel()
        {
            //ModelType = typeof(TEntity);
            Properties = new Dictionary<PropertyInfo, string>();
        }
        public Type ModelType { get; set; }
        public Dictionary<PropertyInfo, string> Properties { get; set; }
        public object ModelInstance { get; set; }
        public string ViewType { get; set; }

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

    public class DynamicEntityComponentViewModel : EntityComponentViewModel
    {
        public DynamicEntityComponentViewModel(object instance):base(instance)
        {

        }
    }
}