using Access.EntityFramework;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        PlayerManager _playerManager = new PlayerManager(new EfPlayerRepository());
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
        public IActionResult ListTeams()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            
            var teams = _teamManager.GetList();
            var model = new List<TeamViewModel>();

            foreach (var team in teams)
            {
                var managerId = team.ManagerId;
                var manager = new Entities.Concrete.Manager();
                if (managerId.HasValue)
                {
                    manager = _managerManager.TGetById(managerId.Value);
                }

                if(team.Id == 1)
                {
                    continue;
                }
                model.Add(new TeamViewModel
                {
                    Id = team.Id,
                    TeamName = team.TeamName,
                    ManagerName = manager.Name == "-" ? "Menajer Yok" : manager.Name + " " + manager.SurName,
                    CreatedTime = team.CreatedTime.Value,
                    UpdatedTime = team.UptatedTime.Value,

                });
            }
            return Json(model);
        }

        [HttpPost]
        public IActionResult GetTeamPlayers(int id)
        {

            var values = _playerManager.GetList().Where(x => x.TeamId == id);

            return Json(values);
        }

        [HttpPost]
        public IActionResult GetAnotherPlayers(int id)
        {
            var values = _playerManager.GetList().Where(x => x.TeamId != id);

            List<PlayerTeamViewModel> players = new List<PlayerTeamViewModel>();

            foreach (var value in values)
            {
                var teamName = _teamManager.TGetById(value.TeamId != null ? value.TeamId.Value : 1).TeamName;
                players.Add(new PlayerTeamViewModel
                {
                    Id = value.Id,
                    Name = value.Name,
                    SurName = value.SurName,
                    County = value.County,
                    Email = value.Email,
                    Phone = value.Phone,
                    TeamId = value.Id,
                    TeamName = teamName,
                });
            }
            return Json(players);
        }

        [HttpPost]
        public IActionResult ChangeTeam(int teamId, int playerId)
        {

            var value = _playerManager.TGetById(playerId);
            value.TeamId = teamId;
            value.UptatedTime = DateTime.Now;
            _playerManager.TUpdate(value);

            return Json(new { success = true, message = "Güncelleme başarılı!" });
        }

        [HttpPost]
        public IActionResult AddTeam([FromBody] Team team)
        {
            if(team != null)
            {
                team.ManagerId = 1;
                team.CreatedTime = DateTime.Now;
                team.UptatedTime = DateTime.Now;
                _teamManager.TAdd(team);
                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "AddPlayer",
                    Message = team.TeamName + " adlı bir takım eklendi.",
                    CreatedTime = DateTime.Now
                });
                return Json(new { success = true, message = "Ekleme başarılı!" });
            }
            else
            {
                return Json(new { success = false, message = "Ekleme başarısız!" });
            }
        }

        [HttpPost]
        public IActionResult DeleteTeam(int teamId)
        {
            var team = _teamManager.TGetById(teamId);
            if(team != null)
            {
                _teamManager.TDelete(team);
                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "AddPlayer",
                    Message = team.TeamName + " adlı bir takım eklendi.",
                    CreatedTime = DateTime.Now
                });
                return Json(new { success = true, message = "Güncelleme başarılı!" });
            }
            else
            {
                return Json(new { success = false, message = "Güncelleme Başarısız!" });
            }
        }
    }
}
