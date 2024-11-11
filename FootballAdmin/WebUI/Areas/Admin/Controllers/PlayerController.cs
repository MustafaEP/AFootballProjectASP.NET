using Access.EntityFramework;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlayerController : Controller
    {
        PlayerManager _playerManager = new PlayerManager(new EfPlayerRepository());
        TeamManager _teamManager = new TeamManager(new EfTeamRepository());
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
        public IActionResult ListPlayers()
        {
            var values = _playerManager.GetList();
            return View(values);
        }

        public IActionResult GetPlayers()
        {
            var values = _playerManager.GetList();
            foreach (var item in values)
            {
                if (item.TeamId != null)
                {
                    item.Team = _teamManager.TGetById(item.TeamId.Value);
                }
            }
            return Json(values);
        }

        public IActionResult GetPlayer(int id)
        {
            var values = _playerManager.TGetById(id);
            if (values != null) return Json(new { success = true, value = values });
            else return Json(new { success = false, message = "Kullanıcı Bulunamadı" });
        }

        [HttpPost]
        public IActionResult DeletePlayer(int id)
        {
            try
            {
                var player = _playerManager.TGetById(id);
                if (player != null)
                {
                    _playerManager.TDelete(player);
                    _adminNotificationManager.TAdd(new AdminNotification
                    {
                        Type = "DeletePlayer",
                        Message = player.Name + " " + player.SurName + " adlı bir oyuncu Silindi.",
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
        public IActionResult UpdatePlayer([FromBody] PlayerUpdateViewModel model)
        {

            if (model != null)
            {

                var oldObject = _playerManager.TGetById(model.Id);
                oldObject.Name = model.Name;
                oldObject.SurName = model.Surname;
                oldObject.TeamId = model.Team;
                oldObject.StrongFoot = model.StrongFoot;
                oldObject.County = model.Country;
                oldObject.Email = model.Email;
                oldObject.Phone = model.Phone;
                oldObject.PlayerPosition = model.Position;
                oldObject.PlayerSecondPositions = model.SecondPosition;
                oldObject.UptatedTime = DateTime.Now;

                _playerManager.TUpdate(oldObject);
                // Güncelleme işlemlerini burada yapabilirsiniz

                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "UpdatePlayer",
                    Message = model.Name + " " + model.Surname + " adlı bir oyuncu güncellendi.",
                    CreatedTime = DateTime.Now
                });
                return Json(new { success = true, message = "Güncelleme başarılı!" });
            }
            return Json(new { success = false, message = "Güncelleme başarısız." });
        }

        [HttpPost]
        public IActionResult AddPlayer([FromBody] PlayerUpdateViewModel model)
        {
            if (model != null)
            {
                var allPlayers = _playerManager.GetList();
                foreach (var item in allPlayers) if (item.UserName == model.UserName) return Json(new { success = false, message = "Bu Kullanıcı Adıyla Bir Oyuncu Bulunuyor" });
                var allManagers = _playerManager.GetList();
                foreach (var item in allManagers) if (item.UserName == model.UserName) return Json(new { success = false, message = "Bu Kullanıcı Adıyla Bir Menajer Bulunuyor" });

                var player = new Entities.Concrete.Player
                {
                    Id = 0,
                    UserName = model.UserName,
                    Name = model.Name,
                    SurName = model.Surname,
                    TeamId = model.Team,
                    StrongFoot = model.StrongFoot,
                    County = model.Country,
                    Email = model.Email,
                    Phone = model.Phone,
                    PlayerPosition = model.Position,
                    PlayerSecondPositions = model.SecondPosition,
                    CreatedTime = DateTime.Now,
                    UptatedTime = DateTime.Now,
                    Password = model.TemporaryPassword != null ? model.TemporaryPassword : "123"
                };

                _playerManager.TAdd(player);
                _adminNotificationManager.TAdd(new AdminNotification
                {
                    Type = "AddPlayer",
                    Message = model.Name + " " + model.Surname + " adlı bir oyuncu eklendi.",
                    CreatedTime = DateTime.Now,
                });
                return Json(new { success = true, message = "Ekleme başarılı!" });
            }
            return Json(new { success = false, message = "Ekleme başarısız." });
        }

    }
}
