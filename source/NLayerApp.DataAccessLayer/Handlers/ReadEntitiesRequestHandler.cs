using MediatR;
using Microsoft.EntityFrameworkCore;
using NLayerApp.DataAccessLayer.Requests;
using NLayerApp.Infrastructure.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Handlers
{
    public class ReadEntitiesRequestHandler<TEntity> : 
        IRequestHandler<ReadEntitiesRequest<TEntity>, IEnumerable<TEntity>>
        where TEntity: class
    {
        EntityRequestHandler<string[], IEnumerable<TEntity>> _innerHandler;
        public ReadEntitiesRequestHandler(IContext context) 
        {
            _innerHandler = new EntityRequestHandler<string[], IEnumerable<TEntity>>(async req =>
            {
                IQueryable<TEntity> set = ((AppDbContext)context).Set<TEntity>();
                if(req.Request != null && req.Request.Length > 0)
                {
                    foreach (var propertyPath in req.Request)
                    {
                        set = set.Include(propertyPath);
                    }
                }
                return await Task.FromResult(set.ToList());
            });
        }

        public async Task<IEnumerable<TEntity>> Handle(ReadEntitiesRequest<TEntity> request, CancellationToken cancellationToken)
        {
            return await _innerHandler.Handle(request, cancellationToken);
        }

    }
}
