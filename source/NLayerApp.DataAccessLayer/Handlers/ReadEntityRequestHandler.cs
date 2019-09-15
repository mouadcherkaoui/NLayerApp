using MediatR;
using NLayerApp.DataAccessLayer.Requests;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Handlers
{
    public class ReadEntityRequestHandler<TEntity, TKey> : IRequestHandler<ReadEntityRequest<TEntity, TKey>, TEntity>
        where TEntity: class, IEntity
    {
        EntityRequestHandler<TKey, TEntity> _innerHandler;
        public ReadEntityRequestHandler(IContext context) 
        {
            _innerHandler = new EntityRequestHandler<TKey, TEntity>(async req =>
            {
                return (await context.GetEntityAsync<TEntity>(req.Request));
            });
        }

        public async Task<TEntity> Handle(ReadEntityRequest<TEntity, TKey> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
