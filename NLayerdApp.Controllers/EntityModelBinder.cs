using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NLayerdApp.Controllers
{
    public class EntityModelBinder : IModelBinder
    {
        Type _entityType;
        public EntityModelBinder(Type entityType)
        {
            _entityType = entityType;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
            //var data = bindingContext.ValueProvider.GetValue("entity");
        }
    }
}