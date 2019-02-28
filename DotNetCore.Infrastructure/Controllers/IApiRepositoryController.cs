using DotNetCore.Infrastructure.Models;

namespace DotNetCore.Infrastructure.Controllers
{
    public interface IApiRepositoryController<TEntity, TKey, TResult>
        where TEntity: IEntity, IEntity<TKey>
    {
         TResult Get();
         TResult Get(TKey id);
         TResult Post(TEntity entity);
         TResult Put(TEntity entity);
         TResult Delete(TKey id);
    }
}