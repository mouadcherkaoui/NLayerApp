using System;
using System.Linq.Expressions;
using System.Reflection;
using NLayerAppp.Infrastructure.Models;

namespace NLayerAppp.Models
{
    public abstract class BaseEntity : IEntity
    {
        public Expression<Func<object,object>> this[string index]
        {
            get{
                Expression<Func<object, object>> exp = m => m.GetType().GetProperty("Length").GetValue(m);

                var type = this.GetType();
                var property = type.GetProperty(index);
                var parameter = Expression.Parameter(type, "m");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);  
                var lambda = Expression.Lambda<Func<object,object>>(propertyAccess, parameter);
                var body = Expression.Property(parameter, property);         
                return lambda;
            }
            set{
                this.GetType().GetProperty(index).SetValue(this, value);
            }
        }

        public object GetPropertyValue(Type modelType, string propertyName)
        {
            return this[propertyName];
        }

        // public void SetPropertyValue(string propertyName, object value)
        // {
        //     this[propertyName] = value;
        // }
        //public abstract TKey Id { get; set; }
        public abstract DateTime CreatedAt { get; set; }
        public abstract DateTime ModifiedAt { get; set; }

        object IEntity.this[string index] => throw new NotImplementedException();
    }
}