using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class EntityRequest<TEntity, TResponse> : IRequest<TResponse>
    {
        // IMappingConfig
        // IMapFrom<>
        // IMapTo<>
        public TEntity _entity;
        public EntityRequest(TEntity entity)
        {
            _entity = entity;
        }
    }
    public class EntityRequestHandler<TEntity, TResponse> : IRequestHandler<EntityRequest<TEntity, TResponse>, TResponse>
    {
        Func<EntityRequest<TEntity, TResponse>, Task<TResponse>> _handlerAction;
        public EntityRequestHandler(Func<EntityRequest<TEntity, TResponse>, Task<TResponse>> handlerAction)
        {
            _handlerAction = handlerAction;
        }
        public async Task<TResponse> Handle(EntityRequest<TEntity, TResponse> request, CancellationToken cancellationToken)
        {
            return await _handlerAction?.Invoke(request);
        }
    }
}
