﻿using System.Linq;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;

namespace NLayerApp.Repositories
{
    public class Repository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TContext: class, IContext
        where TEntity: class, IEntity, IEntity<TKey>
    {
        private TContext _context;
        public Repository(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetEntities()  
        {
            var entities = _context.GetAll<TEntity, TKey>();
            return entities;
        }

        public async Task<TEntity> GetEntity(TKey key)
        {
            return await _context.GetEntityAsync<TEntity, TKey>(key);
        }

        public TEntity AddEntity(TEntity entity)
        {
            var newEntity = _context.Add<TEntity, TKey>(entity).Result;
            _context.Save();
            return newEntity;
        }

        public async Task<bool> DeleteEntityAsync(TKey key)
        {
            var result = await _context.DeleteEntity<TEntity, TKey>(key);
            _context.Save();
            return result;
        }

        public TEntity UpdateEntity(TEntity entity)
        {
            entity = _context.UpdateEntity<TEntity, TKey>(entity).Result;
            _context.Save();
            return entity;
        }

    }
}
