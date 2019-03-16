using NLayerdApp.Infrastructure.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace NLayerdApp.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContext _context;
        public HomeController(IContext context)
        {            
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}