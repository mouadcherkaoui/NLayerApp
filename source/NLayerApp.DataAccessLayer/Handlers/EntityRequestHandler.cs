using MediatR;
using NLayerApp.DataAccessLayer.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Handlers
{
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
