using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainingController : Controller
    {

        TrainingManager _trainingManager = new TrainingManager(new EfTrainingRepository());
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        PlayerManager _playerManager = new PlayerManager(new EfPlayerRepository());
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        public IActionResult ListTraining()
        {
            return View();
        }
        public IActionResult GetTrainings()
        {
            var values = _trainingManager.GetList();
            
            
            List<TrainngViewModel> model = new List<TrainngViewModel>();
            
            foreach (var item in values)
            {
                item.Team = _teamManager.TGetById(item.TeamId);

                string players = item.Players == null ? "[]" : item.Players;
                players = players.Trim('[', ']');

                List<int> playerIds = players.Split(',')
                    .Select(int.Parse).ToList();
                string playersText = "|| ";

                List<string> playersList = new List<string>();
                foreach (var playerId in playerIds)
                {
                    var playerObject = _playerManager.TGetById(playerId);
                    var playerName = playerObject.Name;
                    var playerSurName = playerObject.SurName;
                    playersText += playerName + " " + playerSurName + " || ";

                    playersList.Add(playerName + " " + playerSurName);
                }
                item.Team.Manager = item.Team.ManagerId != null ? _managerManager.TGetById(item.Team.ManagerId.Value) : _managerManager.TGetById(1);
                model.Add(new TrainngViewModel
                {
                    Id = item.Id,
                    TeamName = item.Team.TeamName,
                    TrainingName = item.TrainingName != null ? item.TrainingName : "",
                    Date = item.Date,
                    Players = playersText,
                    ManagerName = item.Team.Manager.Name + " " + item.Team.Manager.SurName,
                    ListPlayers = playersList
                });
            }

            return Json(model);
        }
    }
}
