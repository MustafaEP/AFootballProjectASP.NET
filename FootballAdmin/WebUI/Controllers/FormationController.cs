using Access.EntityFramework.AddedFootballers;
using Access.EntityFramework.ClubFormation;
using Access.EntityFramework.NewVersion;
using Business.Abstract.AddedFootballers;
using Business.Concrete.ClubFormation;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Manager
{
    public class FormationController : Controller
    {
        private readonly ManagerClubManager _clubManager;
        private readonly FormationManager _formationManager;
        private readonly XIPlayerManagere _xiPlayerManager;
        private readonly AddedFootballerManager _addedFootballerManager;
        private readonly FootballerManager _footballerManager;

        public FormationController()
        {
            _clubManager = new ManagerClubManager(new EfManagerClubRepository());
            _formationManager = new FormationManager(new EfFormationRepository());
            _xiPlayerManager = new XIPlayerManagere(new EfXIPlayerRepository());
            _addedFootballerManager = new AddedFootballerManager(new EfAddedFootballerRepository());
            _footballerManager = new FootballerManager(new EfFootballerRepository());
        }

        [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
        public IActionResult FormationPartial(int clubId)
        {
            var club = _clubManager.TGetById(clubId);
            if (club == null)
            {
                return PartialView("_Error", "Kulüp bulunamadı.");
            }

            var formation = _formationManager.GetByClubId(club.Id);
            if (formation == null)
            {
                return PartialView("_Error", "Kulüp Formasyonu bulunamadı.");
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

            return PartialView("_Formation", formation);
        }
    }
}
