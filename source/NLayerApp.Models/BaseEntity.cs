using System;
using System.Linq.Expressions;
using System.Reflection;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Models
{
    public abstract class BaseEntity : IEntity
    {

        // public void SetPropertyValue(string propertyName, object value)
        // {
        //     this[propertyName] = value;
        // }
        //public abstract TKey Id { get; set; }
        public abstract DateTime CreatedAt { get; set; }
        public abstract DateTime ModifiedAt { get; set; }
    }
}