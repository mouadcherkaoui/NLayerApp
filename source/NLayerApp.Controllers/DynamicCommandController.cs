using NLayerApp.Controllers.Attributes;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Models;
using NLayerApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;

namespace NLayerApp.Controllers.Rest
{
    [Route("api/[controller]")]
    [GenericControllerNameConvention(typeof(DynamicApiController<,>))]
    public class DynamicApiController<TEntity, TKey> : ApiCommandController<TEntity, TKey>
        where TEntity: class, IEntity, IEntity<TKey>       
    {
        public DynamicApiController(IMediator mediator):base(mediator)
        { }

        [HttpGet()]
        public override async Task<IActionResult> Get() => await base.Get();


        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(TKey id) => await base.Get(id);

        [HttpPost]
        public override async Task<IActionResult> Post([FromBody]TEntity entity) => await base.Post(entity);

        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(TKey id,[FromBody]TEntity entity) => await base.Put(id, entity);

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(TKey id) => await base.Delete(id);
    }
}