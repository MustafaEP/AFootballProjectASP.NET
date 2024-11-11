using Access.EntityFramework;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        AdminManager _adminManager = new AdminManager(new EfAdminRepository());
        ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {

            var userManager = _managerManager.Login(username, password);

            if (userManager != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userManager.UserName),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim("UserId", userManager.Id.ToString()),
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json(new { success = true, message = "Ho�geldin " + userManager.Name + ", Giri� Sayfas�na Y�nlendiriliyorsun", role = "Manager" });
            }

            var userAdmin = _adminManager.Login(username, password);


            if (userAdmin != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userAdmin.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("UserId", userAdmin.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false // Kullan�c�y� hat�rlamak i�in
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);



                return Json(new { success = true, message = "Ho�geldin " + userAdmin.UserName + " Giri� Sayfas�na Y�nlendiriliyorsun", role = "Admin" });
            }





            return Json(new { success = false, message = "Kullan�c� Bulunamad�" });



        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Kullan�c�y� ��k�� yapma

            // Y�nlendirme yerine sadece ba�ar�l� sonu� d�nebilirsiniz
            return Json(new { success = true, message = "��k�� Ger�ekle�tirildi." });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
