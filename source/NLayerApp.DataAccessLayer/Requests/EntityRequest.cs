using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerApp.DataAccessLayer.Requests
{
    public class EntityRequest<TEntity, TResponse> : IRequest<TResponse>
    {
        // IMappingConfig
        // IMapFrom<>
        // IMapTo<>
        public TEntity Request;
        public EntityRequest(TEntity entity)
        {
            Request = entity;
        }
    }
}
