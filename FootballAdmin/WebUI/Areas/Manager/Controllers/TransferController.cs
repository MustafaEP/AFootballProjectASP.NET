using Access.Abstract.FootballerAbstract;
using Access.Abstract.FootballerAbstract.Statistics;
using Access.EntityFramework.AddedFootballers;
using Access.EntityFramework.FootballerEntity;
using Access.EntityFramework.FootballerEntity.Statistics;
using Access.EntityFramework.NewVersion;
using Business.Abstract.AddedFootballers;
using Business.Abstract.FootballerAbstract;
using Business.Concrete.NewVersion;
using Entities.Concrete.AddedFootballersExplorer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class TransferController : Controller
    {
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());
        ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
        ScoreofFootballerManager _scoreofFootballerManager = new ScoreofFootballerManager(new EfScoreofFootballerRepository());

        AddedFootballerManager _addedFootballerManager = new AddedFootballerManager(new EfAddedFootballerRepository());
        AddedFootballerUpgradeManager _upgradeManager = new AddedFootballerUpgradeManager(new EfAddedFootballerUpgradeRepository());   

        FootballerStatisticManager _statisticManager = new FootballerStatisticManager(new EfFootballerStatisticRepository());
        FootballerAbilityManager _abilityManager = new FootballerAbilityManager(new EfFootballerAbilityRepository());
        FootballerPositionManager _positionManager = new FootballerPositionManager(new EfFootballerPositionRepository());

        FootballerDefendingManager _defendingManager = new FootballerDefendingManager(new EfFootballerDefendingRepository());
        FootballerPaceManager _paceManager = new FootballerPaceManager(new EfFootballerPaceRepository());
        FootballerPassingManager _passingManager = new FootballerPassingManager(new EfFootballerPassingRepository());
        FootballerPhysicalityManager _physicalityManager = new FootballerPhysicalityManager(new EfFootballerPhysicalityRepository());
        FootballerDribblingManager _dribblingManager = new FootballerDribblingManager(new EfFootballerDribblingRepository());
        FootballerShootingManager _shootingManager = new FootballerShootingManager(new EfFootballerShootingRepository());

        public IActionResult GetFootballers()
        {
            var values = _footballerManager.GetListEveryThink();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Döngüleri işler
                WriteIndented = true // Daha okunabilir JSON formatı
            };

            var json = JsonSerializer.Serialize(values, options);
            return Content(json, "application/json");

        }

        public IActionResult GetFootballer(int id)
        {
            var values = _footballerManager.GetEveryThink(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Döngüleri işler
                WriteIndented = true // Daha okunabilir JSON formatı
            };

            var json = JsonSerializer.Serialize(values, options);
            return Json(json);
        }

        [HttpPost]
        public IActionResult AddFootballerForClub(int id)
        {
            

            return Json(new { success = true, message = "Oyuncu Satın Alındı" });
        }


        [HttpGet]
        public IActionResult Footballer(int id)
        {
            var values = _footballerManager.TGetById(id);
            if(values == null)
            {
                return Redirect("/Error");
            }

            values.Statistic = _statisticManager.GetStatistic(id);

            values.FootballerPositions = _positionManager.GetPositions(id);

            var statisticId = values.Statistic.Id;

            values.Statistic.ShootingDetails = _shootingManager.GetStatistic(statisticId);
            values.Statistic.DefendingDetails = _defendingManager.GetStatistic(statisticId);
            values.Statistic.DribblingDetails = _dribblingManager.GetStatistic(statisticId);
            values.Statistic.PaceDetails = _paceManager.GetStatistic(statisticId);
            values.Statistic.PassingDetails = _passingManager.GetStatistic(statisticId);
            values.Statistic.PhysicalityDetails = _physicalityManager.GetStatistic(statisticId);

            values.Statistic.footballerAbilities = _abilityManager.GetAbilities(statisticId);

            return View(values);
        }



        public IActionResult BuyFootballer(int id)
        {
            var footballerId = id;

            var footballer = _footballerManager.TGetById(footballerId);

            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if(managerId == 0)
            {
                return View("Error");
            }

            var club = _managerClubManager.OwnClub(managerId);
            if (club.Budget < footballer.MarketPrice)
                return Json(new { success = false, message = "Paran Yok Kardeş Para Yoook" });
            if (_addedFootballerManager.isAlone(club.Id, footballerId))
                return Json(new { success = false, message = "Zaten Bu Oyuncuya Sahipsin" });

            AddedFootballer added = new AddedFootballer
            {
                ClubId = club.Id,
                FootballerId = footballerId,
                DateAdded = DateTime.Now,
            };

            _addedFootballerManager.TAdd(added);
            club.Budget -= footballer.MarketPrice;
            _managerClubManager.TUpdate(club);

            var addedFootballar = _addedFootballerManager.GetAddedFootballer(club.Id, footballerId);
            AddedFootballerUpgrade upgradeModel = new AddedFootballerUpgrade
            {
                Confidence = 0,
                AddedFootballerId = addedFootballar.Id,
                DefendingChange = 0,
                DribblingChange = 0,
                PaceChange = 0,
                PassingChange = 0,
                PhysicalityChange = 0,
                ShootingChange = 0,
                UpgradeDate = DateTime.Now,
            };
            _upgradeManager.TAdd(upgradeModel);

            return Json(new {success = true, message = "Futbolcu alimı gerçekleşti"});


        }

    }
}
