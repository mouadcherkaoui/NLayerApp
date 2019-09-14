using MediatR;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class GetEntityRequestHandler<TEntity> : IRequestHandler<EntityRequest<object, TEntity>, TEntity>
        where TEntity: class, IEntity
    {
        EntityRequestHandler<object, TEntity> _innerHandler;
        public GetEntityRequestHandler(IContext context) 
        {
            _innerHandler = new EntityRequestHandler<object, TEntity>(async req =>
            {
                return (await context.GetEntityAsync<TEntity>(req._entity));
            });
        }

        public async Task<TEntity> Handle(EntityRequest<object, TEntity> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
