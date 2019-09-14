using MediatR;
using NLayerApp.Infrastructure.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class GetEntitiesRequestHandler<TEntity> : 
        IRequestHandler<EntityRequest<bool, IEnumerable<TEntity>>, IEnumerable<TEntity>>
        where TEntity: class
    {
        EntityRequestHandler<bool, IEnumerable<TEntity>> _innerHandler;
        public GetEntitiesRequestHandler(IContext context) 
        {
            _innerHandler = new EntityRequestHandler<bool, IEnumerable<TEntity>>(async req =>
            {
                return await Task.FromResult(((AppDbContext)context).Set<TEntity>());
            });
        }

        public async Task<IEnumerable<TEntity>> Handle(EntityRequest<bool, IEnumerable<TEntity>> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
