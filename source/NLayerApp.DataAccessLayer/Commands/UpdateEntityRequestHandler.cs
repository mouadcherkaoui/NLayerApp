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
    public class UpdateEntityRequestHandler<TEntity> : IRequestHandler<EntityRequest<TEntity, TEntity>, TEntity>
        where TEntity: class, IEntity
    {
        EntityRequestHandler<TEntity, TEntity> _innerHandler;
        IContext _context;

        public UpdateEntityRequestHandler(IContext context) 
        {
            _context = context;
            _innerHandler = new EntityRequestHandler<TEntity, TEntity>(async req =>
            {
                return  await _context.UpdateEntity<TEntity>(req._entity);
            });
        }

        public async Task<TEntity> Handle(EntityRequest<TEntity, TEntity> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
