using System.Linq;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity, IEntity<TKey>
    {
        TEntity AddEntity(TEntity entity);
        Task<bool> DeleteEntityAsync(params object[] keys);
        Task<TEntity> GetEntity(params object[] keys);
        IQueryable<TEntity> GetEntities();
        TEntity UpdateEntity(TEntity entity);         
    }    
}