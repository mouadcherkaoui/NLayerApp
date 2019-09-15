using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.DataAccessLayer.Requests
{
    public class UpdateEntityRequest<TEntity, TKey> : EntityRequest<KeyValuePair<TKey, TEntity>, TEntity>
    {
        public UpdateEntityRequest(KeyValuePair<TKey, TEntity> entity) : base(entity)
        {
        }
    }
}
