using Access.EntityFramework;
using Access.EntityFramework.AddedFootballers;
using Access.EntityFramework.NewVersion;
using Business.Abstract.AddedFootballers;
using Business.Concrete;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());
        ManagerClubManager _clubManager = new ManagerClubManager(new EfManagerClubRepository());
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        AddedFootballerManager _addedManager = new AddedFootballerManager(new EfAddedFootballerRepository());

        public IActionResult Index()
        {
            var totalPlayer = _footballerManager.TCount();
            var totalClub= _clubManager.TCount();
            var totalManager = _managerManager.TCount();
            var totalAdded = _addedManager.TCount();

            var model = new DashboardViewModel
            {
                PlayerCount = totalPlayer ,
                TeamCount = totalClub,
                ManagerCount = totalManager,
                AddedFootballerCount = totalAdded
            };

            return View(model);
        }

    }
}
