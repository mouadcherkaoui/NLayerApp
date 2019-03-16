using NLayerdApp.Controllers;
using NLayerdApp.DataAccessLayer;
using NLayerdApp.Infrastructure.Repositories;
using NLayerdApp.Models;

namespace NLayerdApp.MvcApp.Controllers
{
    public class MembersController : ApiRepositoryController<AppDataContext, Member, int>
    {
        public MembersController(IRepository<Member, int> repository) : base(repository)
        {
        }
    }
}