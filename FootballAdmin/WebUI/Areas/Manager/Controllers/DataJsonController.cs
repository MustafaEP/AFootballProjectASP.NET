using Access.EntityFramework.NewVersion;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUI.Areas.Manager.Models;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager")]
    public class DataJsonController : Controller
    {

        ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
        ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
        ScoreofFootballerManager _scoreofFootballerManager = new ScoreofFootballerManager(new EfScoreofFootballerRepository());

        public IActionResult GoalAsist()
        {
            var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var club = _managerClubManager.OwnClub(managerId);
            var footballers = _clubFootballerManager.OwnFootballers(club.Id);

            List<GoalAsistViewModel> model = new List<GoalAsistViewModel>();

            foreach (var footballer in footballers)
            {
                footballer.ScoreofFootballer = _scoreofFootballerManager.GetScoresFootballer(footballer.Id);
                model.Add(new GoalAsistViewModel
                {
                    Id = footballer.Id,
                    FullName = footballer.Name + " " + footballer.SurName,
                    Goal = footballer.ScoreofFootballer.Goal,
                    Asist = footballer.ScoreofFootballer.Asist
                });
            }


            return Json(model);
        }

        public IActionResult GetAvatars()
        {
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "saul_theme", "dist", "assets", "media", "avatars");

            if (!Directory.Exists(imageDirectory))
            {
                return Json(new { success = false, message = "Avatarlar Bulunamadı!" });
            }

            var images = Directory.GetFiles(imageDirectory)
                .Where(file => file.EndsWith(".jpg"))
                .Select(file => Path.GetRelativePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), file))
                .Select(relativePath => "/" + relativePath.Replace("\\", "/"))
                .ToList();

            return Json(new { success = true, value = images });
        }

        public IActionResult GetLogos()
        {
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "saul_theme", "src", "media", "features-logos");

            if (!Directory.Exists(imageDirectory))
            {
                return Json(new { success = false, message = "Logolar Bulunamadı!" });
            }

            var images = Directory.GetFiles(imageDirectory)
                .Where(file => file.EndsWith(".png"))
                .Select(file => Path.GetRelativePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), file))
                .Select(relativePath => "/" + relativePath.Replace('\\', '/'))
                .ToList();

            return Json(new { success = true, value = images});
        }

    }
}
