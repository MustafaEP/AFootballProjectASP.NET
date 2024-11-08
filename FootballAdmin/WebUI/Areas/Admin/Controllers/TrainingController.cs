using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;
using WebUI.Areas.Admin.Models.MiniModels;

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

                string accept = item.Accepts == null ? "[]" : item.Accepts;
                accept = accept.Trim('[', ']');

                List<int> playerIdsForName = players.Split(',')
                    .Select(int.Parse).ToList();

                List<int> playerIdsForAccept = accept.Split(',')
                    .Select(int.Parse).ToList();

                List<NameAcceptMiniModel> playerAndAccept = new List<NameAcceptMiniModel>();
                foreach (var playerId in playerIdsForName)
                {

                    var playerObject = _playerManager.TGetById(playerId);
                    if (playerObject == null) continue;
                    var playerName = playerObject.Name;
                    var playerSurName = playerObject.SurName;

                    playerAndAccept.Add(new NameAcceptMiniModel
                    {
                        Name = playerName + " " + playerSurName,
                        Accept = isAccepted(playerId , playerIdsForAccept)
                    });
                }
                item.Team.Manager = item.Team.ManagerId != null ? _managerManager.TGetById(item.Team.ManagerId.Value) : _managerManager.TGetById(1);
                model.Add(new TrainngViewModel
                {
                    Id = item.Id,
                    TeamName = item.Team.TeamName,
                    TrainingName = item.TrainingName != null ? item.TrainingName : "",
                    Date = item.Date,
                    ManagerName = item.Team.Manager.Name + " " + item.Team.Manager.SurName,
                    players = playerAndAccept
                });
            }

            return Json(model);
        }
        public bool isAccepted(int Id, List<int> accepts)
        {
            foreach (var item in accepts)
            {
                if(item == Id) return true;
            }

            return false;
        }

        public IActionResult DeleteTraining(int id)
        {
            var values = _trainingManager.TGetById(id);
            if(values != null)
            {
                _trainingManager.TDelete(values);
                return Json(new { success = true, message = "Antrenman Silindi" });
            }
            else
            {
                return Json(new { success = false, message = "Antrenman Bulunmadı" });
            }
        }
    }
}
