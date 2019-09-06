using System.Collections.Generic;
using System.Reflection;

namespace NLayerApp.DynamicModelDefnition
{
    public class DynamicFieldType
    {
        public readonly object TypeCode;

        public virtual int DynamicFieldTypeId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual TypeInfo FieldType { get; set; }
        public virtual DynamicTypeCode FieldTypeCode { get; set; }
        public virtual string TypeFullName { get; set; }
        public virtual bool IsRelatedType { get; set; }
        public virtual bool IsRelatedCollection { get; set; }
        public virtual string RelatedTypeName { get; set; }
        public virtual IEnumerable<DynamicValidationRule> ValidationRules { get; set; }
    }
}