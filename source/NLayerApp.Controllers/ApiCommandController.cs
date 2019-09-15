using System;
using System.Threading.Tasks;
using NLayerApp.Infrastructure.CQRS;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using NLayerApp.DataAccessLayer.Commands;
using NLayerApp.DataAccessLayer.Requests;
using NLayerApp.Controllers.Attributes;

namespace NLayerApp.Controllers
{
    [Route("api/[controller]")]
    [GenericControllerNameConvention(typeof(ApiCommandController<,>))]

    public class ApiCommandController<TEntity, TKey>: Controller
    {
        IMediator _mediator;
        public ApiCommandController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Get()
        {
            string[] propertiesToInclude = Request.Query.ContainsKey("include") ?
                Request.Query["include"].ToArray() : null;

            var result = await _mediator.Send(new ReadEntitiesRequest<TEntity>(propertiesToInclude));
            return new OkObjectResult(result);
        }
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TKey id)
        {
            var result = await _mediator.Send(new ReadEntityRequest<TEntity, TKey>(id));
            return new OkObjectResult(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            var result = await _mediator.Send(new CreateEntityRequest<TEntity>(entity));
            return new CreatedResult($"api/{typeof(TEntity).Name}", result);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(TKey id, [FromBody] TEntity entity)
        {
            var result = await _mediator.Send(new UpdateEntityRequest<TEntity, TKey>(new KeyValuePair<TKey, TEntity>(id, entity)));
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            var result = await _mediator.Send(new DeleteEntityRequest<TEntity, TKey>(id));
            return new OkObjectResult(result);
        }
    }
}