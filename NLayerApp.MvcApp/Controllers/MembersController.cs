using NLayerApp.Controllers;
using NLayerApp.DataAccessLayer;
using NLayerApp.Infrastructure.Repositories;
using NLayerApp.Models;

namespace NLayerApp.MvcApp.Controllers
{
    public class MembersController : ApiRepositoryController<AppDataContext, Member, int>
    {
        public MembersController(IRepository<Member, int> repository) : base(repository)
        {
        }
    }
}