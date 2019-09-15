using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.DataAccessLayer.Requests
{
    public class ReadEntitiesRequest<TEntity> : EntityRequest<string[], IEnumerable<TEntity>>
    {
        public ReadEntitiesRequest(string[] entity) : base(entity)
        {
        }
    }
}
