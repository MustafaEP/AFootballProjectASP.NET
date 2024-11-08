using Access.EntityFramework;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;
using WebUI.Areas.Admin.Models.MiniModels;


namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
        PlayerManager _playerManager = new PlayerManager(new EfPlayerRepository());
        public IActionResult ListManagers()
        {
            return View();
        }
        public IActionResult GetManagers()
        {
            var managers = _managerManager.GetList();


            List<ManagerDataViewModel> model = new List<ManagerDataViewModel>();

            foreach (var manager in managers)
            {
                if (manager.Id == 1)
                {
                    continue;
                }
                else
                {
                    if (manager?.TeamId != null)
                    {
                        var team = _teamManager.TGetById(manager.TeamId.Value);
                        model.Add(new ManagerDataViewModel
                        {
                            Id = manager.Id,
                            Name = manager.Name,
                            SurName = manager.SurName,
                            Email = manager.Email,
                            Phone = manager.Phone,
                            Country = manager.County,
                            PreferredLineUp = manager.PreferredLineUp,
                            TeamName = team.TeamName,
                            CreatedDate = manager.CreatedTime.Value,
                            UpdatedDate = manager.UptatedTime.Value,
                            TeamId = manager.TeamId != null ? manager.TeamId.Value : 1
                        });
                    }
                    else
                    {
                        model.Add(new ManagerDataViewModel
                        {
                            Id = manager.Id,
                            Name = manager.Name,
                            SurName = manager.SurName,
                            Email = manager.Email,
                            Phone = manager.Phone,
                            Country = manager.County,
                            PreferredLineUp = manager.PreferredLineUp,
                            CreatedDate = manager.CreatedTime.Value,
                            UpdatedDate = manager.UptatedTime.Value,
                            TeamName = "-"
                        });
                    }
                }
            }
            return Json(model);
        }

        [HttpPost]
        public IActionResult DeleteManager(int id)
        {
            try
            {
                var player = _managerManager.TGetById(id);
                if (player != null)
                {
                    _managerManager.TDelete(player);
                    _adminNotificationManager.TAdd(new AdminNotification
                    {
                        Type = "DeleteManager",
                        Message = player.Name + " " + player.SurName + " adlı bir menejer silindi.",
                        CreatedTime = DateTime.Now
                    });
                    return Json(new { success = true, message = "Silme işlemi başarılı." });
                }
                else
                {
                    return Json(new { success = false, message = "Kayıt bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateManager([FromBody] ManagerDataViewModel model)
        {
            var allManagers = _managerManager.GetList().Where(manager => manager.Id != model.Id);
            foreach (var item in allManagers) if (item.UserName == model.UserName) return Json(new { success = false, message = "Bu Kullanıcı Adında Menajer Bulunuyor." });

            var allPlayers = _playerManager.GetList();
            foreach (var item in allPlayers) if (item.UserName == model.UserName) return Json(new { success = false, message = "Bu Kullanıcı Adında Bir Oyuncu Bulunuyor" });


            String changedPerson = "";

            if (model.TeamId == 1)
            {
                var team = _teamManager.TGetById(model.TeamId);
                team.ManagerId = 1;
                team.UptatedTime = DateTime.Now;
                _teamManager.TUpdate(team);
            }
            else
            {
                var team = _teamManager.TGetById(model.TeamId);
                var oldTeam = _teamManager.GetList().Where(x => x.ManagerId == model.Id);
                foreach (var item in oldTeam)
                {
                    item.ManagerId = 1;
                    item.UptatedTime = DateTime.Now;
                    _teamManager.TUpdate(item);
                }
                if (team.ManagerId != null && team.ManagerId != 1 && team.ManagerId != model.Id)
                {
                    var anotherManager = _managerManager.TGetById(team.ManagerId.Value);
                    anotherManager.TeamId = 1;
                    changedPerson = anotherManager.Name + " " + anotherManager.SurName;
                    anotherManager.UptatedTime = DateTime.Now;
                    _managerManager.TUpdate(anotherManager);
                }

                team.ManagerId = model.Id;
                team.UptatedTime = DateTime.Now;
                _teamManager.TUpdate(team);


            }

            if (model != null)
            {

                var objectManager = _managerManager.TGetById(model.Id);
                objectManager.Name = model.Name;
                objectManager.SurName = model.SurName;
                objectManager.TeamId = model.TeamId;
                objectManager.County = model.Country;
                objectManager.Email = model.Email;
                objectManager.Phone = model.Phone;
                objectManager.PreferredLineUp = model.PreferredLineUp;
                objectManager.UptatedTime = DateTime.Now;

                _managerManager.TUpdate(objectManager);

                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "UpdateManager",
                    Message = model.Name + " " + model.SurName + " adlı bir menejer güncellendi.",
                    CreatedTime = DateTime.Now
                });

                // Güncelleme işlemlerini burada yapabilirsiniz
                if (changedPerson != "")
                {

                    return Json(new { success = true, message = "Güncelleme başarılı!", changed = changedPerson });
                }
                else
                {

                    return Json(new { success = true, message = "Güncelleme başarılı!" });
                }
            }
            return Json(new { success = false, message = "Güncelleme başarısız." });
        }

        [HttpPost]
        public IActionResult AddManager([FromBody] ManagerDataViewModel model)
        {
            if (model != null)
            {
                var allManagers = _managerManager.GetList();
                foreach (var item in allManagers) if (item.UserName == model.UserName) return Json(new { success = false, message = "Bu Kullanıcı Adında Menajer Bulunuyor." });

                var allPlayers = _playerManager.GetList();
                foreach (var item in allPlayers) if (item.UserName == model.UserName) return Json(new { success = false, message = "Bu Kullanıcı Adında Bir Oyuncu Bulunuyor" });

                var manager = new Manager
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    SurName = model.SurName,
                    TeamId = model.TeamId,
                    County = model.Country,
                    Email = model.Email,
                    Phone = model.Phone,
                    PreferredLineUp = model.PreferredLineUp,
                    CreatedTime = DateTime.Now,
                    UptatedTime = DateTime.Now,
                    Password = model.TemporaryPassword != null ? model.TemporaryPassword : "123"
                };

                _managerManager.TAdd(manager);

                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "AddManager",
                    Message = manager.Name + " " + manager.SurName + " adlı bir menejer eklendi.",
                    CreatedTime = DateTime.Now
                });

                return Json(new { success = true, message = "Ekleme başarılı!" });
            }
            return Json(new { success = false, message = "Ekleme başarısız." });
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _teamManager.GetList();
            List<IdNameMiniModel> teamInfos = new List<IdNameMiniModel>();

            foreach (var item in teams)
            {
                teamInfos.Add(new IdNameMiniModel
                {
                    Id = item.Id,
                    Name = item.TeamName
                });
            }
            return Json(teamInfos);
        }
    }
}
