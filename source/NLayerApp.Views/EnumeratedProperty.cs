using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NLayerApp.Views
{
    public class BaseEntityt
    {
        public object this[string index]{
            get{
                var value = EnumeratedPropertyCache.FirstOrDefault(p => p.Name == index).Value 
                    ?? this.GetType().GetProperty(index).GetValue(this, null);
                return  value;
            }
        }
        protected IDictionary<string, PropertyInfo> PropertyInfoCache { get; set; }
        protected IEnumerable<EnumeratedProperty> EnumeratedPropertyCache { get; set; }
        private readonly Dictionary<PropertyInfo, string> _properties;// = new Dictionary<PropertyInfo, string>();
        private object _instance;
        public BaseEntityt(object instance)
        {
            _instance = instance;
            var entityType = instance != null && instance is IEnumerable<object> 
                ? instance.GetType().GetGenericArguments()[0]
                : instance.GetType();
            PropertyInfoCache = entityType.GetProperties().ToDictionary(p => p.Name);
            
            EnumeratedPropertyCache = 
                PropertyInfoCache.Select(p => 
                    new EnumeratedProperty(p.Key, 
                        instance == null ?
                            null : 
                            p.Value.GetValue(instance, null)));

            _properties = GetPropertiesWithEditorsTypes(entityType);
        }

        public Type Type { get => _instance.GetType(); }
        public object ModelInstance { get => _instance; }
        public string ViewType { get; set; }
        public Dictionary<PropertyInfo, string> Properties { get => _properties; } 

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

    public class BaseEntityt<T> 
    {
        private T _instance;
        public BaseEntityt(T instance)
        {
            _instance = instance;
            //PropertyInfoCache = typeof(T).GetProperties();
            //EnumeratedPropertyCache = PropertyInfoCache.Select(p => new EnumeratedProperty(p.Name, p.GetValue(instance)));
        }

        public Type Type { get => typeof(T); }
        public T ModelInstance { get => _instance; }
    }

    public class EnumeratedProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; }
        public EnumeratedProperty(string propertyName, object propertyValue)
        {
            Name = propertyName;
            Value = propertyValue;
            Type = propertyValue?.GetType() ?? typeof(object);
        }
    }
}