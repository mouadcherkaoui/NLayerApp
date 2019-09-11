using NLayerApp.Infrastructure.CQRS;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DataAccessLayer.Commands
{
    public class DeleteEntityCommand<TEntity, TKey> : IAsyncCommandHandler<bool>
        where TEntity: class, IEntity<TKey>
    {
        // TDTObject _dtobject;
        // TDTOMappingResolver resolver
        TEntity _entity;
        IContext context;
        public DeleteEntityCommand(TEntity entity)
        {
            _entity = entity;
        }
        public async Task<bool> ExecuteAsync()
        {
            using (context = new AppDataContext())
            {
                var result = await context.DeleteEntity<TEntity, TKey>(_entity.Id);
                await context.SaveAsync();
                return result; 
            }
        }
    }
}
