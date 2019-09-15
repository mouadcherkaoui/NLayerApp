using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.DataAccessLayer.Requests
{
    public class CreateEntityRequest<TEntity> : EntityRequest<TEntity, TEntity>
    {
        public CreateEntityRequest(TEntity entity) : base(entity)
        {
        }
    }
}
