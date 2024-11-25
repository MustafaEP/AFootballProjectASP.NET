using Access.EntityFramework;
using Access.EntityFramework.NewVersion;
using Business.Concrete;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
        ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
        public IActionResult Index()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _managerManager.TGetById(managerId);

            if(manager.ManagerClubId != null)
            {
				manager.ManagerClub = _managerClubManager.TGetById(manager.ManagerClubId.Value);
                manager.ManagerClub.Footballers = _clubFootballerManager.OwnFootballers(manager.ManagerClubId.Value);
			}

            return View(manager);
        }

        public IActionResult Profile()
        {
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _managerManager.TGetById(managerId);

            if(manager.ManagerClubId != null)
            {
                manager.ManagerClub = _managerClubManager.OwnClub(managerId);
            }

            return View(manager);
        }

        [HttpGet]
        public IActionResult ProfileSettings()
        {
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var manager = _managerManager.TGetById(managerId);

			if (manager.ManagerClubId != null)
			{
				manager.ManagerClub = _managerClubManager.OwnClub(managerId);
			}

			return View(manager);
		}
    }
}
