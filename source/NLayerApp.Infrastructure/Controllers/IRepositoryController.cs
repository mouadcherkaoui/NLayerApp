using NLayerApp.Infrastructure.Models;
using System.Threading.Tasks;

namespace NLayerApp.Infrastructure.Controllers
{
    public interface IRepositoryController<TEntity, TKey, TResult>
        where TEntity: IEntity<TKey>
    {
         TResult Index();
         Task<TResult> Index(TKey id);
        //  TResult Details(TKey id);
         TResult Add();
         TResult Add(TEntity entity);
         TResult Update(TKey id);
         TResult Update(TKey id, TEntity entity);
         Task<TResult> Delete(TKey id);
    }
}