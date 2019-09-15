using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.DataAccessLayer.Requests
{
    public class ReadEntityRequest<TEntity, TKey> : EntityRequest<TKey, TEntity>
    {
        public ReadEntityRequest(TKey entity) : base(entity)
        {
        }
    }
}
