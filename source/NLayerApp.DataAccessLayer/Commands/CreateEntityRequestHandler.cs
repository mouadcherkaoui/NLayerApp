using MediatR;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class CreateEntityRequestHandler<TEntity> : IRequestHandler<EntityRequest<TEntity, TEntity>, TEntity>
        where TEntity: class, IEntity
    {
        EntityRequestHandler<TEntity, TEntity> _innerHandler;
        public CreateEntityRequestHandler() 
        {
            _innerHandler = new EntityRequestHandler<TEntity, TEntity>(async req =>
            {
                return await (new AppDbContext("", new Type[] { typeof(TEntity) })).AddEntity<TEntity>(req._entity);
            });
        }

        public async Task<TEntity> Handle(EntityRequest<TEntity, TEntity> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
