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
using Microsoft.EntityFrameworkCore.Metadata;

namespace NLayerApp.DataAccessLayer
{
    public class AppDbContext : DbContext, IContext
    {
        private readonly string _connectionString;
        private readonly Type[] _types;
         
        public AppDbContext(string connectionString, Type[] types)
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
            var applyConfig = modelBuilder.GetType()
                .GetMethods().Where(m => m.Name.StartsWith("ApplyConfiguration"))
                .FirstOrDefault();

            foreach (var current in _types)
            {
                if (modelBuilder.Model.FindEntityType(current) == null)
                {
                    var entityTypeBuilder = modelBuilder.Entity(current);
                }
                var attribute = current.GetCustomAttributes<TypeConfigurationAttribute>().FirstOrDefault();
                if (attribute != null)
                    applyConfig.MakeGenericMethod(current).Invoke(modelBuilder, new[] { Activator.CreateInstance(attribute.ConfigurationType) });
            }
        }

        #region IContext Members

        public Type[] RegisteredTypes { get => _types; }
        //public Dictionary<IEntityType, IProperty[]> Key { get=> Model.GetEntityTypes().ToDictionary(t => t.DefiningEntityType, t => t.FindPrimaryKey().Properties.ToArray());  }
        public async Task<TEntity> GetEntityAsync<TEntity>(params object[] keys) where TEntity : class, IEntity
        {
            var entity = await Set<TEntity>().FindAsync(keys);
            return entity;
        }        

        public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return Set<TEntity>()
                ?.Where(expression);
        }        
        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public async Task<TEntity> AddEntity<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            entity.CreatedAt = DateTime.Now;
            var entry = await Set<TEntity>().AddAsync(entity);
            return entry.Entity;
        }

        public async Task<TEntity> UpdateEntity<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            object[] keyValues = GetKeys(entity);
            var entry = await Set<TEntity>().FindAsync(keyValues);

            foreach (var current in entity.GetType().GetProperties().Where(p => p.Name != "Item"))
            {
                current.SetValue(entry, current.GetValue(entity));
            }

            entry.ModifiedAt = DateTime.Now;
            entry.CreatedAt = entry.CreatedAt;
            //entry.State = EntityState.Modified;
            return entry;
        }

        private object[] GetKeys<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return Model.FindEntityType(typeof(TEntity))
                    .FindPrimaryKey().Properties
                    .Select(p => typeof(TEntity).GetProperty(p.Name).GetValue(entity))
                    .ToArray();
        }

        public async Task<bool> DeleteEntity<TEntity>(params object[] keys) where TEntity : class, IEntity
        {
            var entityToRemove =
                await Set<TEntity>().FindAsync(keys);

            if(entityToRemove == null)
                return false;
            
            Set<TEntity>().Remove(entityToRemove);
            return true;
        }      

        public bool DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {        
            Set<TEntity>().Remove(entity);
            return true;
        }        

        public void Save()
        {
            SaveChanges();
        }

        public bool DeleteEntities<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }


        #endregion
    }
}
