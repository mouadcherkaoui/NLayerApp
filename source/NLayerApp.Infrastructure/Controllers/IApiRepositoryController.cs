using NLayerApp.Infrastructure.Models;
using System.Threading.Tasks;

namespace NLayerApp.Infrastructure.Controllers
{
    public interface IApiRepositoryController<TEntity, TKey, TResult>
        where TEntity: IEntity, IEntity<TKey>
    {
         TResult Get();
         Task<TResult> Get(TKey id);
         TResult Post(TEntity entity);
         TResult Put(TEntity entity);
         Task<TResult> Delete(TKey id);
    }
}