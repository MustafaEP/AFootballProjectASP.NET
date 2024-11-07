using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
        public IActionResult ListNotifications()
        {
            return View();
        }

        public IActionResult GetNotifications()
        {
            var values = _adminNotificationManager.GetList().OrderByDescending(x => x.CreatedTime).ToList();
            return Json(values);
        }
    }
}
