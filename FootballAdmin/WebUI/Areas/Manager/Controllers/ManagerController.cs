using Access.EntityFramework.ClubFormation;
using Access.EntityFramework.NewVersion;
using Business.Concrete.ClubFormation;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        ManagerClubManager _clubManager = new ManagerClubManager(new EfManagerClubRepository());
        FormationManager _formationManager = new FormationManager(new EfFormationRepository());
        public IActionResult Rivals()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var rivals = _clubManager.AnotherClubs(managerId); 
            
            foreach (var r in rivals)
            {
                r.Formation = _formationManager.GetByClubId(r.Id);
            }

            return View(rivals);
        }

        public IActionResult Rival(int id)
        {
            var club = _clubManager.TGetById(id);
            return View(club);
        }
    }
}
