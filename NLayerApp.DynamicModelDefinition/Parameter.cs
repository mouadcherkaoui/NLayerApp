using System;

namespace NLayerApp.DynamicModelDefnition
{
    public class Parameter
    {
        public virtual int ParameterId { get; set; }
        public virtual string Name { get; set; }
        public virtual TypeCode ParameterType { get; set; }
        public virtual object ParameterValue { get; set; }
    }
}