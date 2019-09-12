using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NLayerApp.DataAccessLayer.Configurations
{
    public class ValidationDefinition<TEntity> : IValidatableObject
    {
        TEntity _entity;
        public ValidationDefinition(TEntity entity)
        {
            _entity = entity;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext = null)
        {
            var validationResults = new List<ValidationResult>();
            validationContext = new ValidationContext(_entity);

            Validator.TryValidateObject(_entity, validationContext, validationResults, true);

            return validationResults;
        }
    }

}
