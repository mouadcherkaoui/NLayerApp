using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Controllers;
using NLayerApp.DataAccessLayer;
using NLayerApp.DataAccessLayer.Commands;
using NLayerApp.Infrastructure.DataAccessLayer;
using NLayerApp.Infrastructure.Repositories;
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
            var result = await _mediator.Send(new EntityRequest<object, Member>((object)id));
            return new OkObjectResult(result);
        }

        public override async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new EntityRequest<bool, IEnumerable<Member>>(false));
            return (new OkObjectResult(result));

            //return base.Get();
        }
    }
}
