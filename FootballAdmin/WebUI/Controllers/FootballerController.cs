using Access.EntityFramework.NewVersion;
using Business.Concrete.NewVersion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class FootballerController : Controller
    {
        FootballerManager _footballerManager = new FootballerManager(new EfFootballerRepository());

        [HttpGet , Authorize(Roles = "Admin, Manager")]
        public IActionResult Footballer(int id)
        {
            var footballer = _footballerManager.GetEveryThink(id);

            if (footballer == null) return View("Error");
            else return View(footballer);
        }

        [HttpGet, Authorize(Roles = "Admin, Manager")]
        public IActionResult FootballerJson(int id)
        {
            var footballer = _footballerManager.GetEveryThink(id);


            if (footballer == null) return View("Error");
            else return Json(footballer);
        }

        [HttpGet, Authorize(Roles = "Admin, Manager")]
        public IActionResult Footballers()
        {
            var footballers = _footballerManager.GetListEveryThink();
            return View(footballers);
        }


    }
}
