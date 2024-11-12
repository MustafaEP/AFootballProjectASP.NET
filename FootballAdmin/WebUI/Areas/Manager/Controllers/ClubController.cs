using Access.EntityFramework;
using Access.EntityFramework.NewVersion;
using Business.Concrete;
using Business.Concrete.NewVersion;
using Entities.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class ClubController : Controller
    {
        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
        ManagerManager _manageerManager = new ManagerManager(new EfManagerRepository());
        
        public IActionResult ClubInfoPage()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return View();
        }

        public IActionResult ClubInfo()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var club = _managerClubManager.OwnClub(managerId);

            if (club == null)
            {
                return Json(new { success = false, message = "Kulüp Oluşturulmamış" });
            }
            else
            {
                return Json(new { success = true, message = "Kulübü Var", values = club });
            }
        }

        [HttpPost]
        public IActionResult AddClub([FromBody] ManagerClub club)
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var allClubs = _managerClubManager.GetList();
            foreach (var item in allClubs)
            {
                if (item.Name == club.Name)
                {
                    return Json(new { success = false, message = "Bu İsimde Bir Kulüp Var" });
                }
            }
            club.CreatedTime = DateTime.Now;
            club.UptatedTime = DateTime.Now;
            club.ManagerId = managerId;
            club.LineUp = "0";

            _managerClubManager.TAdd(club);

            var addedClub = _managerClubManager.GetList().FirstOrDefault(x => x.ManagerId == managerId);
            if(addedClub != null)
            {
                var manager = _manageerManager.TGetById(managerId);
                manager.ManagerClubId = addedClub.Id;
                _manageerManager.TUpdate(manager);
            }

            return Json(new { success = true, message = "Kulüp Başarıyla Eklendi" });
        }
    }
}
