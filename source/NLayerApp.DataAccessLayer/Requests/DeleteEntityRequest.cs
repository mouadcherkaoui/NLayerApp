using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.DataAccessLayer.Requests
{
    public class DeleteEntityRequest<TEntity, TKey> : EntityRequest<TKey, TEntity>
    {
        public DeleteEntityRequest(TKey entity) : base(entity)
        {
        }
    }
}
