using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Models;
using MediatR;
using NLayerApp.DataAccessLayer.Requests;
using NLayerApp.Controllers.Rest;

namespace NLayerApp.MvcApp.Controllers
{
    [Route("api/member")]
    public class MembersController : DynamicApiController<Member, int>
    {
        IMediator _mediator;
        public MembersController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        public override async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _mediator.Send(new ReadEntityRequest<Member, int>(id));

            return new OkObjectResult(result);
        }

        public override async Task<IActionResult> Get()
        {
            var includedProperties = Request.Query.ContainsKey("include") ? Request.Query["include"].ToArray() : null;
            var result = await _mediator.Send(new ReadEntitiesRequest<Member>(includedProperties));

            return (new OkObjectResult(result));

            //return base.Get();
        }
    }
}
