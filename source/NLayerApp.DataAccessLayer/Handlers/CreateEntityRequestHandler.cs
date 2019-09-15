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
    public class CreateEntityRequestHandler<TEntity> : IRequestHandler<CreateEntityRequest<TEntity>, TEntity>
        where TEntity: class, IEntity
    {
        EntityRequestHandler<TEntity, TEntity> _innerHandler;
        IContext _context;
        public CreateEntityRequestHandler(IContext context) 
        {
            _context = context;
            _innerHandler = new EntityRequestHandler<TEntity, TEntity>(async req =>
            {
                var result = await _context.AddEntity<TEntity>(req.Request);
                await _context.SaveAsync();
                return result; 
            });
        }

        public async Task<TEntity> Handle(CreateEntityRequest<TEntity> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
