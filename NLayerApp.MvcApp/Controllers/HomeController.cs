using NLayerAppp.Infrastructure.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace NLayerAppp.MvcApp.Controllers
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