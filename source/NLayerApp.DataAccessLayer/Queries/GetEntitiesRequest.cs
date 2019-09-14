using NLayerApp.Infrastructure.CQRS;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Queries
{
    public class GetEntitiesRequest<TEntity> : IAsyncCommandHandler<IEnumerable<TEntity>>
        where TEntity: class, IEntity
    {
        IContext _context;
        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            using (_context = new AppDataContext())
            {
                var result = _context.GetEntities<TEntity>();
                return result;
            }
        }
    }
}
