using NLayerApp.Infrastructure.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace NLayerApp.MvcApp.Controllers
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