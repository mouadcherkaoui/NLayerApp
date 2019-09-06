using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Infrastructure.Controllers
{
    public interface IRepositoryController<TEntity, TKey, TResult>
        where TEntity: IEntity<TKey>
    {
         TResult Index();
         TResult Index(TKey id);
        //  TResult Details(TKey id);
         TResult Add();
         TResult Add(TEntity entity);
         TResult Update(TKey id);
         TResult Update(TKey id, TEntity entity);
         TResult Delete(TKey id);
    }
}