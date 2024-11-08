using Access.EntityFramework;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MatchController : Controller
    {
        MatchManager _matchManager = new MatchManager(new EfMatchRepository());
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
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
                if (item.HomeTeam.ManagerId != null)
                {
                    item.HomeTeam.Manager = _managerManager.TGetById(item.HomeTeam.ManagerId.Value);
                }
                if (item.AwayTeam.ManagerId != null)
                {
                    item.AwayTeam.Manager = _managerManager.TGetById(item.AwayTeam.ManagerId.Value);
                }
            }

            return Json(values);
        }
        [HttpPost]
        public IActionResult DeleteMatch(int id)
        {
            var value = _matchManager.TGetById(id);
            if (value != null)
            {
                _matchManager.TDelete(value);
                value.HomeTeam = _teamManager.TGetById(value.HomeTeamId);
                value.AwayTeam = _teamManager.TGetById(value.AwayTeamId);
                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "DeleteMatch",
                    Message = value.HomeTeam.TeamName + " " + value.AwayTeam.TeamName + " maçı iptal edildi.",
                    CreatedTime = DateTime.Now
                });
                return Json(new { success = true, message = "Maç Silindi" });
            }
            else
            {
                return Json(new { success = false, message = "Maç Silinemedi" });
            }
        }

        [HttpPost]
        public IActionResult AddMatch([FromBody] Match match)
        {
            if (match != null)
            {
                match.HomeTeam = _teamManager.TGetById(match.HomeTeamId);
                match.AwayTeam = _teamManager.TGetById(match.AwayTeamId);
                if (match.HomeTeamId == match.AwayTeamId)
                {
                    return Json(new { success = false, message = "Bir Takım Kendisiyle Oyniyamaz :D" });
                }

                var homeTeamAnotherMatches = _matchManager.GetList().Where(x => (x.HomeTeamId == match.HomeTeamId) || (x.AwayTeamId == match.HomeTeamId));
                foreach(var anotherMatch in homeTeamAnotherMatches)
                {
                    if(anotherMatch.MatchDate == match.MatchDate)
                    {
                        return Json(new { success = false, message = "Aynı Anda Ev Sahibi " + match.HomeTeam.TeamName + "'ın Maçı Var", });
                    }
                }
                var awayTeamAnotherMatches = _matchManager.GetList().Where(x => (x.HomeTeamId == match.AwayTeamId) || (x.AwayTeamId == match.AwayTeamId));
                foreach (var anotherMatch in awayTeamAnotherMatches)
                {
                    if (anotherMatch.MatchDate == match.MatchDate)
                    {
                        return Json(new { success = false, message = "Aynı Anda Deplasman Takımı " + match.HomeTeam.TeamName + "'ın Maçı Var", });
                    }
                }


               
                _matchManager.TAdd(new Match
                {
                    HomeTeamId = match.HomeTeamId,
                    AwayTeamId = match.AwayTeamId,
                    MatchDate = match.MatchDate
                });
                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "AddMatch",
                    Message = match.HomeTeam.TeamName + " " + match.AwayTeam.TeamName + "'ye karşı.",
                    CreatedTime = DateTime.Now,
                });
                return Json(new { success = true, message = "Maç Eklendi.", teams = match.HomeTeam.TeamName + " - " + match.AwayTeam.TeamName });
            }
            else
            {
                return Json(new { success = false, message = "Takımlar Eklenemedi. Veri Gönderim Hatası" });
            }
        }
    }
}
