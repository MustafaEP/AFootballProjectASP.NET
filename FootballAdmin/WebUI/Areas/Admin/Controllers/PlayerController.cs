using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlayerController : Controller
    {
        PlayerManager _playerManager = new PlayerManager(new EfPlayerRepository());
        public IActionResult ListPlayers()
        {
            var values = _playerManager.GetList();
            return View(values);
        }
    }
}
