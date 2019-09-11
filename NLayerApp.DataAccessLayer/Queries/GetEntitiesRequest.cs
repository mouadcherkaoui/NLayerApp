using NLayerApp.Infrastructure.CQRS;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Queries
{
    public class GetEntitiesRequest<TEntity, TKey> : IAsyncCommandHandler<IEnumerable<TEntity>>
        where TEntity: class, IEntity<TKey>
    {
        IContext _context;
        TKey _key;
        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            using (_context = new AppDataContext())
            {
                var result = await _context.GetEntitiesAsync<TEntity, TKey>();
                return result;
            }
        }
    }
}
