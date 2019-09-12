using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Models;
using Microsoft.EntityFrameworkCore;
using NLayerApp.DataAccessLayer.Configurations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer
{
    public class AppDbContext : DbContext, IContext
    {
        private readonly string _connectionString;
        private readonly Dictionary<Type, Type> _types;
        private readonly Type[] _typeConfigs;

        public AppDbContext(string connectionString, Dictionary<Type, Type> types)
        {
            _connectionString = connectionString;
            _types = types;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Debugger.Break();
            var applyConfig = modelBuilder.GetType()
                .GetMethods().Where(m => m.Name.StartsWith("ApplyConfiguration"))
                .FirstOrDefault();

            foreach (var current in _types)
            {
                if (modelBuilder.Model.FindEntityType(current.Key) == null)
                {
                    var entityTypeBuilder = modelBuilder.Entity(current.Key);
                    if(current.Value != null)
                        applyConfig.MakeGenericMethod(current.Key).Invoke(modelBuilder, new[] { Activator.CreateInstance(current.Value) });
                }
            }
        }

        #region IContext Members

        public async Task<TEntity> GetEntityAsync<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>
        {
            return await base.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(key));
        }        

        public IQueryable<TEntity> GetAll<TEntity, TKey>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TKey>
        {
            return this.Set<TEntity>()
                ?.Where(expression);
        }        
        public IQueryable<TEntity> GetEntitiesAsync<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            return base.Set<TEntity>();
        }

        public async Task<TEntity> Add<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>
        {
            entity.CreatedAt = DateTime.Now;
            var entry = await Set<TEntity>().AddAsync(entity);
            return entry.Entity;
        }

        public async Task<TEntity> UpdateEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>
        {
            var entry = await Set<TEntity>().FindAsync(entity.Id);
            foreach (var current in entity.GetType().GetProperties().Where(p => p.Name != "Item"))
            {
                current.SetValue(entry, current.GetValue(entity));
            }
            entry.ModifiedAt = DateTime.Now;
            entry.CreatedAt = entry.CreatedAt;
            //entry.State = EntityState.Modified;
            return entry;
        }

        public async Task<bool> DeleteEntity<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>
        {
            var entityToRemove = 
                await Set<TEntity>()
                    ?.FirstOrDefaultAsync(e => e.Id.Equals(key));
            if(entityToRemove == null)
                return false;
            
            this.Set<TEntity>().Remove(entityToRemove);
            return true;
        }      

        public bool DeleteEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>
        {        
            this.Set<TEntity>().Remove(entity);
            return true;
        }        

        public void Save()
        {
            base.SaveChanges();
        }

        public bool DeleteEntities<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<TKey>
        {
            base.RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }


        #endregion
    }
}
