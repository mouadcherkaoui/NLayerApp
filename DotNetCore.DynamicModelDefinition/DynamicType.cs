using System.Collections.Generic;

namespace DotNetCore.DynamicModelsDefnition
{
    public class DynamicType
    {
        public virtual int DynamicTypeId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual  string InterfaceType { get; set; }
        public virtual IEnumerable<DynamicType> RelatedTypes { get; set; }
        public virtual IEnumerable<IEnumerable<DynamicType>> RelatedCollections { get; set; }
        public virtual IEnumerable<DynamicFieldType> Fields { get; set; }
        public virtual IEnumerable<DynamicBusinessRule> BusinessRules { get; set; }
    }
}