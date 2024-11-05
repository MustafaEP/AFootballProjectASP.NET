using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MatchController : Controller
    {
        MatchManager _matchManager = new MatchManager(new EfMatchRepository());
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        public IActionResult Matches()
        {
            return View();
        }

        public IActionResult GetMatches()
        {
            var values = _matchManager.GetList();
            foreach (var item in values)
            {
                item.HomeTeam = _teamManager.TGetById(item.HomeTeamId);
                item.AwayTeam = _teamManager.TGetById(item.AwayTeamId);
            }

            return Json(values);
        }
    }
}
