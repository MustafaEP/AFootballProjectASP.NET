using Access.EntityFramework;
using Access.EntityFramework.NewVersion;
using Business.Concrete;
using Business.Concrete.NewVersion;
using Entities.Concrete;
using Entities.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebUI.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using Business.Abstract.AddedFootballers;
using Access.Abstract.FootballerAbstract;
using Access.Abstract.FootballerAbstract.Statistics;
using Business.Abstract.FootballerAbstract;
using Access.EntityFramework.AddedFootballers;
using Access.EntityFramework.FootballerEntity;
using Access.EntityFramework.FootballerEntity.Statistics;
using Entities.Concrete.ClubFormation;
using Business.Concrete.ClubFormation;
using Access.EntityFramework.ClubFormation;  // Dosya işlemleri için gerekli


namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class ClubController : Controller
    {
        ManagerManager _manageerManager = new ManagerManager(new EfManagerRepository());

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


        FormationManager _formationManager = new FormationManager(new EfFormationRepository());
        XIPlayerManagere _xiPlayerManager = new XIPlayerManagere(new EfXIPlayerRepository());

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
                club.Footballers = _clubFootballerManager.OwnFootballers(club.Id);
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
            club.LineUp = "{\"formation\":\"4-4-2\",\"footballers\":[0,0,0,0,0,0,0,0,0,0,0]}";

            _managerClubManager.TAdd(club);

            var addedClub = _managerClubManager.GetList().FirstOrDefault(x => x.ManagerId == managerId);
            if (addedClub != null)
            {
                var manager = _manageerManager.TGetById(managerId);
                manager.ManagerClubId = addedClub.Id;
                _manageerManager.TUpdate(manager);
            }

            return Json(new { success = true, message = "Kulüp Başarıyla Eklendi" });
        }


        public IActionResult ClubPlayers()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _manageerManager.TGetById(managerId);
            var footballers = _clubFootballerManager.OwnFootballers(manager.ManagerClubId.Value);

            return View(footballers);
        }


        public IActionResult ClubPlayersJSON(int id)
        {
            var footballer = _clubFootballerManager.TGetById(id);
            return Json(footballer);
        }
        public IActionResult ClubPlayerMap()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMapData()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var manager = _manageerManager.TGetById(managerId);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "countries.json");

            string countriesJsonString = System.IO.File.ReadAllText(filePath);

            var countries = JsonConvert.DeserializeObject<Dictionary<string, CountryViewModel>>(countriesJsonString);

            var data = new List<dynamic>();

            foreach (var country in countries)
            {
                var countryData = country.Value;
                data.Add(new { id = country.Key, country = countryData.Name_tr, value = _clubFootballerManager.HowManyPlayerinCountry(countryData.Name, manager.ManagerClubId.Value) });
            }

            return Json(data);
        }


        public IActionResult Club()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var manager = _manageerManager.TGetById(managerId);

            if (manager.ManagerClubId != null)
            {
                manager.ManagerClub = _managerClubManager.TGetById(manager.ManagerClubId.Value);
            }

            return View(manager);
        }

        [HttpPost]
        public IActionResult Club(ManagerClub club)
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);



            var oldClub = _managerClubManager.OwnClub(managerId);
            if (oldClub == null)
            {
                return Json(new { success = false, message = "Kullanıcının Kulübü Bulunamadı" });
            }

            oldClub.Avatar = club.Avatar;
            oldClub.Name = club.Name;

            _managerClubManager.TUpdate(oldClub);

            return Json(new { success = true, message = "Güncelleme Başarılı" });
        }


        public IActionResult ClubFormation()
        {
            return View();
        }

        public IActionResult ClubInfoForFormation()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (managerId == 0)
            {
                return Json(new { success = false, message = "Kullanıcı Girişi Yapmalınız!" });
            }

            var club = _managerClubManager.OwnClub(managerId);
            if (club == null)
            {
                return Json(new { success = false, message = "Kulübünüz Bulunamadı" });
            }


            var addedFootballers = _addedFootballerManager.GetAddedsClub(club.Id);
            if (addedFootballers == null)
            {
                return Json(new { success = false, message = "Oyuncu Satın Alımı Daha Gerçekleştirmemişsiniz!" });
            }


            foreach (var addedfootballer in addedFootballers)
            {
                addedfootballer.Footballer = _footballerManager.GetEveryThink(addedfootballer.FootballerId);
              

            }

            return Json(new { success = true, value = addedFootballers });
        }

        [HttpPost]
        public IActionResult FormationPost([FromBody] FormationViewModel model)
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (managerId == 0) return Json(new { success = "false", message = "Kullanıcı Girişiniz Zaman Aşımına Uğramıştır!!!" });
            if (model == null) return Json(new { success = "false", message = "Diziliş Kaydedilemedi!!!" });

            var club = _managerClubManager.OwnClub(managerId);
            var formation = new Formation();

            formation.ManagerClubId = club.Id;
            formation.FotmationType = model.FormationString;
            if (formation.XIPlayers == null)
            {
                formation.XIPlayers = new List<XIPlayer>();
            }
            for (var i = 0; i < 11; i++)
            {
                formation.XIPlayers.Add(new XIPlayer
                {
                    Position = model.Positions[i],
                    AddedFootballerId = model.IDs[i] != null ? Convert.ToInt32(model.IDs[i]) : null
                });
            }

            var getFormation = _formationManager.GetByClubId(club.Id);

            if (getFormation == null)
            {
                _formationManager.TAdd(formation);
                getFormation = _formationManager.GetByClubId(club.Id);
            }
            else
            {
                getFormation.FotmationType = formation.FotmationType;
                _formationManager.TUpdate(getFormation);
                var xi = _xiPlayerManager.GetFormationPlayer(getFormation.Id);

                for (var i = 0; i < 11; i++)
                {
                    xi[i].Position = model.Positions[i];
                    xi[i].AddedFootballerId = model.IDs[i] != null ? Convert.ToInt32(model.IDs[i]) : null;
                }

                _xiPlayerManager.UpdateXI(xi);

            }
            return Json(new { success = true, message = "Formasyon Kaydedildi." });

        }


        [HttpGet]
        public IActionResult GetFormation()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var club = _managerClubManager.OwnClub(managerId);

            var formation = _formationManager.GetByClubId(club.Id);
            if (formation == null)
            {
                return Json(new { });
            }
            formation.XIPlayers = _xiPlayerManager.GetFormationPlayer(formation.Id);
            foreach (var player in formation.XIPlayers)
            {
                if (player.AddedFootballerId != null)
                {
                    player.AddedFootballer = _addedFootballerManager.TGetById(player.AddedFootballerId.Value);
                    player.AddedFootballer.Footballer = _footballerManager.GetEveryThink(player.AddedFootballer.FootballerId);
                }
            }
            return Json(new { value = formation });
        }

        public IActionResult YourClub()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var club = _managerClubManager.OwnClub(managerId);

            var addedFootballers = _addedFootballerManager.GetAddedsClub(club.Id);

            foreach(var footballer in addedFootballers)
            {
                footballer.Footballer = _footballerManager.GetEveryThink(footballer.FootballerId);
            }
            var footballers = new List<Footballer>();
            foreach(var footballer in addedFootballers)
            {
                footballers.Add(footballer.Footballer);
            }


            return View(footballers);
        }

    }
}
