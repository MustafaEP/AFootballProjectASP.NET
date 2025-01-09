using Access.EntityFramework.NewVersion;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Manager.ViewComponents
{
    [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
    public class AcceptedMatchesViewComponent : ViewComponent
    {
        FootbalMatchManager _matchManager = new FootbalMatchManager(new EfFootballMatchRepository());
        ManagerClubManager _clubManager = new ManagerClubManager(new EfManagerClubRepository());
        public IViewComponentResult Invoke(int clubId)
        {
            var matches = _matchManager.GetAcceptedClubMatches(clubId);
            foreach (var match in matches)
            {
                match.HomeClub = _clubManager.TGetById(match.HomeClubId);
                match.AwayClub = _clubManager.TGetById(match.AwayClubId);
            }
            return View(matches);
        }
    }
}
