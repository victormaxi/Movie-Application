using Microsoft.AspNetCore.Mvc;

namespace Movie_Client.Controllers
{
    public class BaseController : Controller
    {
        private readonly HttpContext _context;

        public BaseController(HttpContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
