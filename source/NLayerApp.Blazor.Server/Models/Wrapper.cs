using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace NLayerApp.Blazor.ServerApp.Models
{
    public class IndexedWrapper<T>: ObservableObject
    {
        Dictionary<string, object> _properties;
        T _value;
        public IndexedWrapper(T value)
        {
            _value = value;
            _properties = typeof(T).GetProperties().ToDictionary(t => t.Name,
                t => t.GetValue(_value));

        }
        public object this[string index]
        {
            get => _properties[index];
            set { var property = _properties[index]; RaisePropertyChanged(ref property, value); }
        }
    }
}