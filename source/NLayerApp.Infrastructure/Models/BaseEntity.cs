using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace NLayerApp.Infrastructure.Models
{
    public class BaseEntityObject : IEntity
    {
        public object this[string index]{
            get{
                var value = EnumeratedPropertyCache.FirstOrDefault(p => p.Name == index)?.Value 
                    ?? this.GetType().GetProperty(index).GetValue(this, null);
                return  value;
            }
        }
        [NotMapped]
        protected IDictionary<string, PropertyInfo> PropertyInfoCache { get; set; }
        [NotMapped]
        protected IEnumerable<EnumeratedProperty> EnumeratedPropertyCache 
            { 
                get => this.GetType().GetProperties().Select(p => new EnumeratedProperty(p.Name, p.GetValue(this))); } 
        private readonly Dictionary<PropertyInfo, string> _properties;// = new Dictionary<PropertyInfo, string>();
        private object _instance;
        public  BaseEntityObject()
        {
            
        }

        [NotMapped]
        public Type Type { get => _instance.GetType(); }
        [NotMapped]
        public object ModelInstance { get => _instance; }
        [NotMapped]
        public string ViewType { get; set; }
        [NotMapped]
        public Dictionary<PropertyInfo, string> Properties { get => _properties; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime ModifiedAt { get; set; }

        private Dictionary<PropertyInfo, string> GetPropertiesWithEditorsTypes(Type entityType)
        {
            var propertiesDictionary = new Dictionary<PropertyInfo, string>();
            var properties = this.GetType().GetProperties();
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