using NLayerAppp.Controllers;
using NLayerAppp.DataAccessLayer;
using NLayerAppp.Infrastructure.Repositories;
using NLayerAppp.Models;

namespace NLayerAppp.MvcApp.Controllers
{
    public class MembersController : ApiRepositoryController<AppDataContext, Member, int>
    {
        public MembersController(IRepository<Member, int> repository) : base(repository)
        {
        }
    }
}