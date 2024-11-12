using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        public IActionResult Index()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _managerManager.TGetById(managerId);

            ViewBag.Name = manager.Name;
            ViewBag.SurName = manager.SurName;



            return View();
        }
    }
}
