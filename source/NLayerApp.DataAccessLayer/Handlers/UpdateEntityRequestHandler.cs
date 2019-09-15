using MediatR;
using NLayerApp.DataAccessLayer.Handlers;
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
    public class UpdateEntityRequestHandler<TEntity, TKey> : IRequestHandler<UpdateEntityRequest<TEntity, TKey>, TEntity>
        where TEntity: class, IEntity
    {
        EntityRequestHandler<KeyValuePair<TKey, TEntity>, TEntity> _innerHandler;
        IContext _context;

        public UpdateEntityRequestHandler(IContext context) 
        {
            _context = context;
            _innerHandler = new EntityRequestHandler<KeyValuePair<TKey, TEntity>, TEntity>(async req =>
            {
                var result = await _context.UpdateEntity<TEntity>(req.Request.Value);
                await _context.SaveAsync();

                return result;
            });
        }

        public async Task<TEntity> Handle(UpdateEntityRequest<TEntity, TKey> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
