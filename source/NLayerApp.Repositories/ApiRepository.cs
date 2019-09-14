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
            var newEntity = _context.AddEntity<TEntity>(entity).Result;
            _context.Save();

            return newEntity;
        }

        public async Task<bool> DeleteEntityAsync(params object[] key)
        {
            var result = await _context.DeleteEntity<TEntity>(key);
            _context.Save();

            return result;
        }

        public IQueryable<TEntity> GetEntities()  
        {
            var entities = _context.GetEntities<TEntity>();

            return entities;
        }

        public async Task<TEntity> GetEntity(params object[] key)
        {
            return await _context.GetEntityAsync<TEntity>(key);
        }

        public TEntity UpdateEntity(TEntity entity)
        {
            entity = _context.UpdateEntity<TEntity>(entity).Result;
            _context.Save();
            
            return entity;
        }
    }
}