using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLayerAppp.Infrastructure.Models;

namespace NLayerAppp.Infrastructure.DataAccessLayer
{
    public interface IContext
    {
        TEntity Add<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>;        
        TEntity Get<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>;

        IQueryable<TEntity> GetAll<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
        IQueryable<TEntity> GetAll<TEntity, TKey>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TKey>;

        TEntity UpdateEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>;        
        bool DeleteEntity<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>;
        bool DeleteEntities<TEntity, TKey>(IEnumerable<TEntity> keys) where TEntity : class, IEntity<TKey>;

        void Save();

    }
}