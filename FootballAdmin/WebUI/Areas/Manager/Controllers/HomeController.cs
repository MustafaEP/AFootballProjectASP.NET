using Access.EntityFramework;
using Access.EntityFramework.NewVersion;
using Business.Concrete;
using Business.Concrete.NewVersion;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUI.Areas.Manager.Models;

namespace WebUI.Areas.Manager.Controllers
{
	[Area("Manager"), Authorize(Roles = "Manager")]
	public class HomeController : Controller
	{
		ManagerManager _managerManager = new ManagerManager(new EfManagerRepository());
		ManagerClubManager _managerClubManager = new ManagerClubManager(new EfManagerClubRepository());
		ClubFootballerManager _clubFootballerManager = new ClubFootballerManager(new EfClubFootballerRepository());
		public IActionResult Index()
		{
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			ViewBag.ManagerId = managerId;	
			var manager = _managerManager.TGetById(managerId);

			if (manager.ManagerClubId != null)
			{
				manager.ManagerClub = _managerClubManager.TGetById(manager.ManagerClubId.Value);
				manager.ManagerClub.Footballers = _clubFootballerManager.OwnFootballers(manager.ManagerClubId.Value);
			}
			

			return View(manager);
		}

		public IActionResult Profile()
		{
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var manager = _managerManager.TGetById(managerId);

			if (manager.ManagerClubId != null)
			{
				manager.ManagerClub = _managerClubManager.OwnClub(managerId);
			}

			return View(manager);
		}

		[HttpGet]
		public IActionResult ProfileSettings()
		{
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var manager = _managerManager.TGetById(managerId);

			if (manager.ManagerClubId != null)
			{
				manager.ManagerClub = _managerClubManager.OwnClub(managerId);
			}

			return View(manager);
		}

		[HttpPost]
		public IActionResult ProfileSettings(Entities.Concrete.Manager manager)
		{
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

			var oldManager = _managerManager.TGetById(managerId);

			var allManagers = _managerManager.GetList();
			if (!_managerManager.PhoneControl(allManagers, manager.Phone, managerId))
				return Json(new { success = false, message = "Telefon Numarası Kullanılıyor" });
			if (!_managerManager.EMailControl(allManagers, manager.Email, managerId))
				return Json(new { success = false, message = "E-Mail Adresi Kullanılıyor" });

			if (oldManager != null)
			{
				oldManager.Name = manager.Name;
				oldManager.SurName = manager.SurName;
				oldManager.County = manager.County;
				oldManager.Email = manager.Email;
				oldManager.Phone = manager.Phone;
				oldManager.UptatedTime = DateTime.Now;
				oldManager.Avatar = manager.Avatar;

				if (oldManager.ManagerClubId != null)
				{
					var club = _managerClubManager.TGetById(oldManager.ManagerClubId.Value);
					club.Name = manager.ManagerClub.Name;
					_managerClubManager.TUpdate(club);
				}
				_managerManager.TUpdate(oldManager);
				return Json(new { success = true, message = "Güncelleme Gerçekleştirildi." });
			}

			return Json(new { success = false, message = "Menejer Bulunamadı. Kullanıcı Çıkışı Yapıp Tekrar Giriniz." });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChangeUsername([FromBody]Entities.Concrete.Manager postManager)
		{
			if (postManager.UserName == null)
				return Json(new { success = false, message = "İnanmam. Yapma Böyle Şeyler. Sence Kullanıcı Adı Boş Olabilirmi 😠" });

			if (postManager.UserName == null)
				return Json(new { success = false, message = "İnanmam. Yapma Böyle Şeyler. Sence Şifre Boş Olabilirmi 😠" });

			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

			var manager = _managerManager.TGetById(managerId);

			if(manager.Password != postManager.Password)
				return Json(new { success = false, message = "Yanlış Şifre! Tekrar Deneyiniz." });

			if (manager == null)
				return Json(new { success = false, message = "Menajer Bulunamadı! Lütfen Sayfayı Yenileyip Tekrar Deneyiniz." });

			manager.UserName = postManager.UserName;

			var allManagers = _managerManager.GetList();
			if (!_managerManager.UserNameControl(allManagers, manager.UserName, managerId))
				return Json(new { success = false, message = "Kullanıcı Adı Kullanılıyor" });

			_managerManager.TUpdate(manager);
			return Json(new { success = true, message = "Güncelleme Başarılı" });

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChangePassword([FromBody]PasswordChangeViewModel model)
		{
			var managerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var manager = _managerManager.TGetById(managerId);

			if(manager != null)
			{
				
				if(model.OldPassword == null)
					return Json(new { success = false, message = "İnanmam. Yapma Böyle Şeyler. Sence Eski Şifre Boş Olabilirmi 😠" });
				if(model.NewPassword == null)
					return Json(new { success = false, message = "İnanmam. Yapma Böyle Şeyler. Sence Yeni Şifre Boş Olabilirmi 😠" });

				if(model.OldPassword != manager.Password)
					return Json(new { success = false, message = "Yanlış Şifre Girdiniz!" });

				if(model.NewPassword ==  manager.Password)
					return Json(new { success = false, message = "Eski Şire ile Yeni Şifre Aynı Olamaz 😠" });

				manager.Password = model.NewPassword;
				_managerManager.TUpdate(manager);
				return Json(new { success = true, message = "Şifre Değiştirildi." });
			}
			return Json(new { success = false, message = "Menejer Bulunamadı. Kullanıcı Çıkışı Yapıp Tekrar Giriniz." });
		}



	}
}
