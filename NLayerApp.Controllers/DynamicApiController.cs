using NLayerApp.Controllers.Attributes;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public override IActionResult Get() => base.Get();

        [HttpGet("{id}")]
        public override IActionResult Get(TKey id) => base.Get(id);

        [HttpPost]
        public override IActionResult Post([FromBody]TEntity entity) => base.Post(entity);

        [HttpPut]
        public override IActionResult Put([FromBody]TEntity entity) => base.Put(entity);

        [HttpDelete("{id}")]
        public override IActionResult Delete(TKey id) => base.Delete(id);
    }
}