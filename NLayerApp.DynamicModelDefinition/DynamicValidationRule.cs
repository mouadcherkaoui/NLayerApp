using System.Collections.Generic;

namespace NLayerApp.DynamicModelDefnition
{
    public class DynamicValidationRule
    {
        public virtual int DynamicValidationRuleId { get; set; }
        public virtual ValidationRule ValidationRule { get; set; }
        public virtual IEnumerable<Parameter> Parameters { get; set; }
    }

    public enum ValidationRule
    {
        Key,
        Required,
        ColumnName,
        MinLength,
        MaxLength,
        DataType,
        EmailAddress,
        Url,
        StringLength,
        Display,
        AutoGeneratedKey,
        NotMapped

    }
}