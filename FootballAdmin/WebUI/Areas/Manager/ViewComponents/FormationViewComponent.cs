using Access.EntityFramework;
using Access.EntityFramework.AddedFootballers;
using Access.EntityFramework.ClubFormation;
using Access.EntityFramework.NewVersion;
using Business.Abstract.AddedFootballers;
using Business.Concrete.ClubFormation;
using Business.Concrete.NewVersion;
using Entities.Concrete.NewVersion;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Manager.ViewComponents
{
    [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
    public class FormationViewComponent : ViewComponent
    {
        ManagerClubManager _clubManager = new ManagerClubManager(new EfManagerClubRepository());
        FormationManager _formationManager = new FormationManager(new EfFormationRepository());
        XIPlayerManagere _xiPlayerManager = new XIPlayerManagere(new EfXIPlayerRepository());
        AddedFootballerManager _addedFootballerManager = new AddedFootballerManager(new EfAddedFootballerRepository());
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());


        
        public IViewComponentResult Invoke(int clubId)
        {

            var club = _clubManager.TGetById(clubId);
            if(club == null)
            {
                return Content("Kulüp bulunamadı.");
            }

            var formation = _formationManager.GetByClubId(club.Id);
            if (formation == null)
            {
                return Content("Kulübün Bir Formasyonu Yok.");
            }
            formation.XIPlayers = _xiPlayerManager.GetFormationPlayer(formation.Id);
            foreach(var player in formation.XIPlayers)
            {
                if (player.AddedFootballerId != null)
                {
                    player.AddedFootballer = _addedFootballerManager.TGetById(player.AddedFootballerId.Value);

                    player.AddedFootballer.Footballer =  _footballerManager.GetEveryThink(player.AddedFootballer.FootballerId);
                    
                }
                    
            }


            return View(formation);

        }
    }
}
