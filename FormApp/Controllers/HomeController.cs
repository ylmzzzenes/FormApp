using Microsoft.AspNetCore.Mvc;

namespace FormApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
