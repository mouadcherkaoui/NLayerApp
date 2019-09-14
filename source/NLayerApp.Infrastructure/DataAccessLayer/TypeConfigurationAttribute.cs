using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.Infrastructure.DataAccessLayer
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class TypeConfigurationAttribute : Attribute
    {
        public TypeConfigurationAttribute(Type configurationType)
        {
            this.ConfigurationType = configurationType;
        }

        public Type ConfigurationType { get; private set; }
    }
}
