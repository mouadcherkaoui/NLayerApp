using System;

namespace NLayerdApp.Infrastructure.Models
{
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