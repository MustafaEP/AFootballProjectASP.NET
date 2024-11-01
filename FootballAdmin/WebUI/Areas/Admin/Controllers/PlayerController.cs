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
        public IActionResult ListPlayers()
        {
            var values = _playerManager.GetList();
            return View(values);
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

                _playerManager.TUpdate(oldObject);
                // Güncelleme işlemlerini burada yapabilirsiniz
                return Json(new { success = true, message = "Güncelleme başarılı!" });
            }
            return Json(new { success = false, message = "Güncelleme başarısız." });
        }

        [HttpPost]
        public IActionResult AddPlayer([FromBody] PlayerUpdateViewModel model)
        {
            if(model != null)
            {
                var player = new Player
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
                    Password = model.TemporaryPassword != null ? model.TemporaryPassword : "123"
                };

                _playerManager.TAdd(player);
                return Json(new { success = true, message = "Ekleme başarılı!" });
            }
            return Json(new { success = false, message = "Ekleme başarısız." });
        }

    }
}
