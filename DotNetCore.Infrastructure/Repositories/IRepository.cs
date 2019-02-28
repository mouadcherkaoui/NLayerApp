using System.Linq;
using DotNetCore.Infrastructure.Models;

namespace DotNetCore.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity, IEntity<TKey>
    {
        TEntity AddEntity(TEntity entity);
        bool DeleteEntity(TKey key);
        TEntity GetEntity(TKey key);
        IQueryable<TEntity> GetEntities();
        TEntity UpdateEntity(TEntity entity);         
    }    
}