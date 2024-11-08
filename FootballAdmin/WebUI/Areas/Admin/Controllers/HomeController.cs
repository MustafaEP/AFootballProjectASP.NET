using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        PlayerManager _playerManager = new PlayerManager(new EfPlayerRepository());
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        MatchManager _matchManager = new MatchManager(new EfMatchRepository());

        public IActionResult Index()
        {
            var totalPlayer = _playerManager.TCount();
            var totalTeam = _teamManager.TCount();
            var totalManager = _managerManager.TCount();
            var match = _matchManager.GetLast();

            var homeTeamName = _teamManager.TGetById(match.HomeTeamId).TeamName;
            var awayTeamName = _teamManager.TGetById(match.AwayTeamId).TeamName;

            var model = new DashboardViewModel
            {
                PlayerCount = totalPlayer ,
                TeamCount = totalTeam,
                ManagerCount = totalManager,
                Match = homeTeamName + " - " + awayTeamName,
                MatchDate = (DateTime)match.MatchDate
            };

            return View(model);
        }

        public IActionResult Settings()
        {
            return View();
        }
        public IActionResult ToDoList()
        {
            return View();
        }
    }
}
