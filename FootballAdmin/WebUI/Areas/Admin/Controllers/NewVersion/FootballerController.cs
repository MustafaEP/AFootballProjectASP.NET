using Access.Abstract.FootballerAbstract.Statistics;
using Access.Abstract.FootballerAbstract;
using Access.EntityFramework.FootballerEntity.Statistics;
using Access.EntityFramework.FootballerEntity;
using Business.Abstract.FootballerAbstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Access.EntityFramework.NewVersion;
using Business.Concrete.NewVersion;
using Entities.Concrete.NewVersion;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers.NewVersion
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class FootballerController : Controller
    {
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());

        FootballerStatisticManager _statisticManager = new FootballerStatisticManager(new EfFootballerStatisticRepository());
        FootballerAbilityManager _abilityManager = new FootballerAbilityManager(new EfFootballerAbilityRepository());
        FootballerPositionManager _positionManager = new FootballerPositionManager(new EfFootballerPositionRepository());

        FootballerDefendingManager _defendingManager = new FootballerDefendingManager(new EfFootballerDefendingRepository());
        FootballerPaceManager _paceManager = new FootballerPaceManager(new EfFootballerPaceRepository());
        FootballerPassingManager _passingManager = new FootballerPassingManager(new EfFootballerPassingRepository());
        FootballerPhysicalityManager _physicalityManager = new FootballerPhysicalityManager(new EfFootballerPhysicalityRepository());
        FootballerDribblingManager _dribblingManager = new FootballerDribblingManager(new EfFootballerDribblingRepository());
        FootballerShootingManager _shootingManager = new FootballerShootingManager(new EfFootballerShootingRepository());

        [HttpGet]
        public IActionResult AddFootballer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFootballer(Footballer footballer)
        {
            var allFootballers = _footballerManager.GetList();
            foreach (var player in allFootballers)
            {
                if (player.username == footballer.username)
                {
                    TempData["ErrorMessage"] = "Bu kullanıcı adıyla bir futbolcu daha önce eklenmiştir.";
                    return View(footballer);
                }
            }

            footballer.CreatedTime = DateTime.Now;
            footballer.UptatedTime = DateTime.Now;
            _footballerManager.TAdd(footballer);
            return RedirectToAction("Footballers", "Footballer", new { area = "Admin" });
        }

        

        
    }
}
