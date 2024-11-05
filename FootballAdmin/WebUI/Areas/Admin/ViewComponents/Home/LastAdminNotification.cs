using Access.EntityFramework;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.ViewComponents.Home
{
    public class LastAdminNotification : ViewComponent
    {
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
        public IViewComponentResult Invoke()
        {
            var model = _adminNotificationManager.GetList().OrderByDescending(x => x.CreatedTime).Take(5).ToList();
            return View(model);
        }
    }
}
