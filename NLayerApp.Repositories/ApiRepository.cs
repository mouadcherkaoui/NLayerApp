using System.Linq;
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
            var newEntity = _context.Add<TEntity, TKey>(entity);
            _context.Save();

            return newEntity;
        }

        public bool DeleteEntity(TKey key)
        {
            var result = _context.DeleteEntity<TEntity, TKey>(key);
            _context.Save();

            return result;
        }

        public IQueryable<TEntity> GetEntities()  
        {
            var entities = _context.GetAll<TEntity, TKey>();

            return entities;
        }

        public TEntity GetEntity(TKey key)
        {
            return _context.Get<TEntity, TKey>(key);
        }

        public TEntity UpdateEntity(TEntity entity)
        {
            entity = _context.UpdateEntity<TEntity, TKey>(entity);
            _context.Save();
            
            return entity;
        }
    }
}