using NLayerApp.Infrastructure.CQRS;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class CreateEntityCommand<TEntity, TKey> : IAsyncCommand<TEntity>
        where TEntity: class, IEntity<TKey>
    {
        // TDTObject _dtobject;
        // TDTOMappingResolver resolver
        TEntity _entity;
        IContext context;
        public CreateEntityCommand(TEntity entity)
        {
            _entity = entity;
        }
        public async Task<TEntity> ExecuteAsync()
        {
            using (context = new AppDataContext())
            {
                var result = await context.Add<TEntity, TKey>(_entity);
                await context.SaveAsync();
                return result; 
            }
        }
    }
}
