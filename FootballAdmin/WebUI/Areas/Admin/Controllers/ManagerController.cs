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
                        TeamName = team.TeamName
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
                        TeamName = "-"
                    });
                }
            }
            return Json(model);
            model = null;
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

                _managerManager.TUpdate(objectManager);
                // Güncelleme işlemlerini burada yapabilirsiniz
                return Json(new { success = true, message = "Güncelleme başarılı!" });
            }
            return Json(new { success = false, message = "Güncelleme başarısız." });
        }

        [HttpPost]
        public IActionResult AddManager([FromBody] ManagerDataViewModel model)
        {
            if (model != null)
            {
                var manager = new Manager
                {
                    Id = 0,
                    UserName = model.UserName,
                    Name = model.Name,
                    SurName = model.SurName,
                    TeamId = model.TeamId,
                    County = model.Country,
                    Email = model.Email,
                    Phone = model.Phone,
                    PreferredLineUp = model.PreferredLineUp,
                    Password = model.TemporaryPassword != null ? model.TemporaryPassword : "123"
                };

                _managerManager.TAdd(manager);
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
