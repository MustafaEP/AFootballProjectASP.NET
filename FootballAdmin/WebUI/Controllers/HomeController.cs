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
                    new Claim(ClaimTypes.NameIdentifier, userManager.Id.ToString()),
                    new Claim(ClaimTypes.Name, userManager.UserName),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim("Name", userManager.Name),
                    new Claim("SurName", userManager.SurName),
                    new Claim("UserId", userManager.Id.ToString()),
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);



                return Json(new { success = true, message = "Hoþgeldin " + userManager.Name + ", Giriþ Sayfasýna Yönlendiriliyorsun", role = "Manager" });
            }

            var userAdmin = _adminManager.Login(username, password);


            if (userAdmin != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userAdmin.Id.ToString()),
                    new Claim(ClaimTypes.Name, userAdmin.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("UserId", userAdmin.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false // Kullanýcýyý hatýrlamak için
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                

                return Json(new { success = true, message = "Hoþgeldin " + userAdmin.UserName + " Giriþ Sayfasýna Yönlendiriliyorsun", role = "Admin" });
            }





            return Json(new { success = false, message = "Kullanýcý Bulunamadý" });



        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Kullanýcýyý çýkýþ yapma

            // Yönlendirme yerine sadece baþarýlý sonuç dönebilirsiniz
            return Json(new { success = true, message = "Çýkýþ Gerçekleþtirildi." });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromBody]Manager manager)
        {
            var allManagers = _managerManager.GetList();
            foreach(var item in allManagers)
            {
                if(item.UserName == manager.UserName) return Json(new { success = false, message = "Bu Kullanýcý Adý Kullanýlýyor." });
            }

            var allAdmins = _adminManager.GetList();
            foreach(var item in allAdmins)
            {
                if (item.UserName == manager.UserName) return Json(new { success = false, message = "Bu Kullanýcý Adý Kullanýlýyor." });
            }

            manager.PreferredLineUp = "0";
            manager.County = "";
            manager.CreatedTime = DateTime.Now;
            manager.UptatedTime = DateTime.Now;
            manager.Phone = "";

            _managerManager.TAdd(manager);

            return Json(new { success = true, message = "Aramýza Hoþgeldin." });
        }

    }
}
