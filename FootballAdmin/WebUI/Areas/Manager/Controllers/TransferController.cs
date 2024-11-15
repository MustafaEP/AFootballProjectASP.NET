using Access.EntityFramework.NewVersion;
using Business.Concrete.NewVersion;
using Entities.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class TransferController : Controller
    {
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());
        ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());

        public IActionResult BuyPlayers()
        {
            return View();
        }

        public IActionResult GetFootballers()
        {
            var values = _footballerManager.GetList();

            return Json(values);
        }

        public IActionResult GetFootballer(int id)
        {
            var values = _footballerManager.TGetById(id);

            return Json(values);
        }

        [HttpPost]
        public IActionResult AddFootballerForClub(int id)
        {
            var footballerId = id;

            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var club = _managerClubManager.OwnClub(managerId);

            var allFootballerInClub = _clubFootballerManager.OwnFootballers(club.Id);

            var footballer = _footballerManager.TGetById(footballerId);

            foreach (var item in allFootballerInClub)
                if (item.username == footballer.username) 
                    return Json(new { success = false, message = "Sahip Olduğunuz Futbolcuyu Tekrar Alamazsınız!" });

            if(club.Budget < footballer.MarketPrice)
                return Json(new { success = false, message = "Oyuncuyu Satın Almak İçin Yeterli Bütçeniz Yok!" });

            club.Budget = club.Budget - footballer.MarketPrice;
            _managerClubManager.TUpdate(club);

            var footballerinTheClub = new ClubsFootballer
            {
                Abilities = footballer.Abilities,
                AltPositions = footballer.AltPositions,
                ClubName = footballer.ClubName,
                Country = footballer.Country,
                Confidence = "",
                CreatedTime = DateTime.Now,
                Defending = footballer.Defending,
                DefendingUpgrade = "",
                Dribbling = footballer.Dribbling,
                DribblingUpgrade = "",
                ManagerClubId = club.Id,
                Name = footballer.Name,
                Pace = footballer.Pace,
                PaceUpgrade = "",
                Passing = footballer.Passing,
                PassingUpgrade = "",
                Physicality = footballer.Physicality,
                PhysicalityUpgrade = "",
                Position = footballer.Position,
                PreferredFoot = footballer.PreferredFoot,
                Shooting = footballer.Shooting,
                ShootingUpgrade = "",
                SurName = footballer.SurName,
                UptatedTime = DateTime.Now,
                username = footballer.username,
            };


            _clubFootballerManager.TAdd(footballerinTheClub);
                

            return Json(new { success = true, message = "Oyuncu Satın Alındı" });
        }
    }
}
