using System.Linq;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity, IEntity<TKey>
    {
        TEntity AddEntity(TEntity entity);
        Task<bool> DeleteEntityAsync(TKey key);
        TEntity GetEntity(TKey key);
        IQueryable<TEntity> GetEntities();
        TEntity UpdateEntity(TEntity entity);         
    }    
}