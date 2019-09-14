using NLayerApp.Infrastructure.CQRS;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class UpdateEntityCommand<TEntity, TKey> : IAsyncCommandHandler<TEntity>
        where TEntity : class, IEntity<TKey>
    {
        // TDTObject _dtobject;
        // TDTOMappingResolver resolver
        TEntity _entity;
        IContext context;
        public UpdateEntityCommand(TEntity entity)
        {
            _entity = entity;
        }
        public async Task<TEntity> ExecuteAsync()
        {
            using (context = new AppDataContext())
            {
                var result = (await context.UpdateEntity<TEntity>(_entity));
                await context.SaveAsync();
                return result;
            }
        }
    }
}
