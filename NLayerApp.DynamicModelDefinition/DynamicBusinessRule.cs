using System.Collections.Generic;

namespace NLayerAppp.DynamicModelsDefnition
{
    public class DynamicBusinessRule
    {
        public virtual int DynamicBusinessRuleId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IEnumerable<DynamicConstraint> Constraints { get; set; }
    }

}