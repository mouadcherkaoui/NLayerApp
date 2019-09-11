using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.Models;

namespace NLayerApp.Infrastructure.DataAccessLayer
{
    public interface IContext: IDisposable
    {
        Task<TEntity> Add<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>;        
        Task<TEntity> GetEntityAsync<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>;

        IQueryable<TEntity> GetAll<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
        IQueryable<TEntity> GetAll<TEntity, TKey>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TKey>;

        Task<TEntity> UpdateEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>;        
        Task<bool> DeleteEntity<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>;
        bool DeleteEntities<TEntity, TKey>(IEnumerable<TEntity> keys) where TEntity : class, IEntity<TKey>;

        void Save();
        Task<int> SaveAsync();

    }
}