using NLayerApp.Infrastructure.CQRS;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Queries
{
    public class GetEntityRequest<TEntity, TKey> : IAsyncCommandHandler<TEntity>
        where TEntity: class, IEntity<TKey>
    {
        IContext _context;
        TKey _key;
        public async Task<TEntity> ExecuteAsync()
        {
            using (_context = new AppDataContext())
            {
                var result = await _context.GetEntityAsync<TEntity, TKey>(_key);
                return result;
            }
        }
    }
}
