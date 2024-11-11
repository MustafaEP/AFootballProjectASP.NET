using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Player.Controllers
{
    [Area("Player")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
