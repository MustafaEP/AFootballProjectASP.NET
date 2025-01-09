using Access.EntityFramework.NewVersion;
using Access.EntityFramework;
using Business.Concrete.NewVersion;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities.Concrete.Matches;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class PlayingController : Controller
    {

        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
        ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
        FootbalMatchManager _footballMatchManager = new FootbalMatchManager(new EfFootballMatchRepository());

        [HttpPost]
        public IActionResult PlayMatch(FootballMatch match)
        {


            return Json(new { });
        }
    }
}
