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
    public class AppDataContext : DbContext, IContext
    {
        private readonly string _connectionString = @"Server=.\;Initial Catalog=nlayerappdb;Integrated Security=True;";         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debugger.Break();
            var assembly = typeof(BaseEntity).GetTypeInfo().Assembly;
            var types = assembly.DefinedTypes.Where(t => t.GetInterfaces().Contains(typeof(IEntity)) && t.IsClass && !t.IsAbstract).ToList();

            var applyConfig = modelBuilder.GetType()
                .GetMethods().Where(m => m.Name.StartsWith("ApplyConfiguration"))
                .FirstOrDefault();

            foreach (var current in types)
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

        public async Task<TEntity> GetEntityAsync<TEntity>(params object[] keys) where TEntity : class, IEntity
        {
            return await base.Set<TEntity>().FindAsync(keys);
        }        

        public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return this.Set<TEntity>()
                ?.Where(expression);
        }        
        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public async Task<TEntity> AddEntity<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            entity.CreatedAt = DateTime.Now;
            var entry = await Set<TEntity>().AddAsync(entity);
            return entry.Entity;
        }

        public async Task<TEntity> UpdateEntity<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var keyValues = Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties
                .Select(p => typeof(TEntity).GetProperty(p.Name).GetValue(entity))
                .ToArray();

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

        public async Task<bool> DeleteEntity<TEntity>(params object[] keys) where TEntity : class, IEntity
        {
            var entityToRemove = 
                await Set<TEntity>()
                    ?.FindAsync(keys);
            if(entityToRemove == null)
                return false;
            
            this.Set<TEntity>().Remove(entityToRemove);
            return true;
        }      

        public bool DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {        
            this.Set<TEntity>().Remove(entity);
            return true;
        }        
        public void Save()
        {
            base.SaveChanges();
        }

        public bool DeleteEntities<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
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
