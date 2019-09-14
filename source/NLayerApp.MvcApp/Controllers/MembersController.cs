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

namespace NLayerApp.MvcApp.Controllers
{
    public class MembersController : ApiRepositoryController<AppDataContext, Member, int>
    {
        IContext _dbContext;
        public MembersController(IRepository<Member, int> repository, IContext context) : base(repository)
        {
            _dbContext = context;
        }

        public override async Task<IActionResult> Get(int id)
        {
            var request = new EntityRequest<object, Member>((object)id);
            var handler = new GetEntityRequestHandler<Member>(_dbContext);
            var result = await handler.Handle(request, new CancellationToken());
            return new OkObjectResult(result);
        }

        public override async Task<IActionResult> Get()
        {
            var request = new EntityRequest<bool, IEnumerable<Member>>(false);
            var handler = new GetEntitiesRequestHandler<Member>(_dbContext);
            var result = await handler.Handle(request, new CancellationToken());
            return (new OkObjectResult(result));

            //return base.Get();
        }
    }
}
