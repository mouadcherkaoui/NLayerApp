using NLayerApp.Controllers.Attributes;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NLayerApp.Controllers.Rest
{
    [Route("api/[controller]")]
    [GenericControllerNameConvention(typeof(DynamicApiController<,,>))]
    public class DynamicApiController<TContext, TEntity, TKey> : ApiRepositoryController<TContext, TEntity, TKey>
        where TContext: class, IContext
        where TEntity: class, IEntity, IEntity<TKey>       
    {
        public DynamicApiController(IRepository<TEntity, TKey> repository):base(repository)
        { }

        [HttpGet()]
        public override async Task<IActionResult> Get() => await base.Get();


        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(TKey id) => await base.Get(id);

        [HttpPost]
        public override IActionResult Post([FromBody]TEntity entity) => base.Post(entity);

        [HttpPut]
        public override IActionResult Put([FromBody]TEntity entity) => base.Put(entity);

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(TKey id) => await base.Delete(id);
    }
}