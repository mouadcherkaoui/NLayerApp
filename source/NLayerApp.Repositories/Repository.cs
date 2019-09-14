using System.Linq;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;

namespace NLayerApp.Repositories
{
    public class Repository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TContext: class, IContext
        where TEntity: class, IEntity, IEntity<TKey>
    {
        private TContext _context;
        public Repository(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetEntities()  
        {
            var entities = _context.GetEntities<TEntity>();
            return entities;
        }

        public async Task<TEntity> GetEntity(params object[] keys)
        {
            return await _context.GetEntityAsync<TEntity>(keys);
        }

        public TEntity AddEntity(TEntity entity)
        {
            var newEntity = _context.AddEntity<TEntity>(entity).Result;
            _context.Save();
            return newEntity;
        }

        public async Task<bool> DeleteEntityAsync(params object[] keys)
        {
            var result = await _context.DeleteEntity<TEntity>(keys);
            _context.Save();
            return result;
        }

        public TEntity UpdateEntity(TEntity entity)
        {
            entity = _context.UpdateEntity<TEntity>(entity).Result;
            _context.Save();
            return entity;
        }

    }
}
