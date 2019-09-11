using System.Linq;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;

namespace NLayerApp.Repositories
{
    public class ApiRepository<TContext, TEntity, TKey> 
        : IRepository<TEntity, TKey>
        where TContext: class, IContext 
        where TEntity: class, IEntity<TKey>, new()
    {
        private TContext _context;
        public ApiRepository(TContext context)
        {
            _context = context;
        }
        public TEntity AddEntity(TEntity entity)
        {
            var newEntity = _context.Add<TEntity, TKey>(entity).Result;
            _context.Save();

            return newEntity;
        }

        public async Task<bool> DeleteEntityAsync(TKey key)
        {
            var result = await _context.DeleteEntity<TEntity, TKey>(key);
            _context.Save();

            return result;
        }

        public async Task<IQueryable<TEntity>> GetEntities()  
        {
            var entities = await _context.GetEntitiesAsync<TEntity, TKey>();

            return entities;
        }

        public async Task<TEntity> GetEntity(TKey key)
        {
            return await _context.GetEntityAsync<TEntity, TKey>(key);
        }

        public TEntity UpdateEntity(TEntity entity)
        {
            entity = _context.UpdateEntity<TEntity, TKey>(entity).Result;
            _context.Save();
            
            return entity;
        }
    }
}