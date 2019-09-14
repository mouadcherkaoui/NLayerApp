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
        Task<TEntity> AddEntity<TEntity>(TEntity entity) where TEntity : class, IEntity;        
        Task<TEntity> GetEntityAsync<TEntity>(params object[] keys) where TEntity : class, IEntity;

        IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class;
        IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

        Task<TEntity> UpdateEntity<TEntity>(TEntity entity) where TEntity : class, IEntity;        
        Task<bool> DeleteEntity<TEntity>(params object[] key) where TEntity : class, IEntity;
        bool DeleteEntities<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Save();
        Task<int> SaveAsync();

    }
}