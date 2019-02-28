using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DotNetCore.Infrastructure.DataAccessLayer;
using DotNetCore.Infrastructure.Models;
using DotNetCore.Models;
using Microsoft.EntityFrameworkCore;


namespace DotNetCore.DataAccessLayer
{
    public class AppDataContext : DbContext, IContext
    {
        private readonly string _connectionString = @"Server=.\;Initial Catalog=dynamicsdb;Integrated Security=True;";         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(BaseEntity).GetTypeInfo().Assembly;
            var types = assembly.DefinedTypes.Where(t => t.GetInterfaces().Contains(typeof(IEntity)) && t.IsClass && !t.IsAbstract).ToList();
            foreach (var current in types)
            {
                if(modelBuilder.Model.FindEntityType(current.AsType()) == null)
                    modelBuilder.Entity(current.AsType());
            }
        }
        #region IContext Members

        public TEntity Get<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>
        {
            return base.Set<TEntity>().FirstOrDefault(e => e.Id.Equals(key));
        }        

        public IQueryable<TEntity> GetAll<TEntity, TKey>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TKey>
        {
            return this.Set<TEntity>()
                ?.Where(expression);
        }        
        public IQueryable<TEntity> GetAll<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            return base.Set<TEntity>();
        }
        public TEntity Add<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>
        {
            entity.CreatedAt = DateTime.Now;
            var entry = this.Set<TEntity>().Add(entity);
            return (TEntity)entry.Entity;
        }

        public TEntity UpdateEntity<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey>
        {
            var entry = this.Set<TEntity>().Find(entity.Id);
            foreach (var current in entity.GetType().GetProperties().Where(p => p.Name != "Item"))
            {
                current.SetValue(entry, current.GetValue(entity));
            }
            entry.ModifiedAt = DateTime.Now;
            entry.CreatedAt = entry.CreatedAt;
            //entry.State = EntityState.Modified;
            return entry;
        }

        public bool DeleteEntity<TEntity, TKey>(TKey key) where TEntity : class, IEntity<TKey>
        {
            var entityToRemove = 
                this.Set<TEntity>()
                    ?.FirstOrDefault(e => e.Id.Equals(key));
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


        #endregion        
    }
}
