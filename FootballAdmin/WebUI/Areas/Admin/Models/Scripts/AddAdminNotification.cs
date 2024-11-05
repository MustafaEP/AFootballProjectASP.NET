using Access.EntityFramework;
using Business.Concrete;
using Entities.Concrete;

namespace WebUI.Areas.Admin.Models.Scripts
{
    public class AddAdminNotification
    {
        AdminNotificationManager _adminNotificationManager = new AdminNotificationManager(new EfAdminNotificationRepository());
        public AddAdminNotification(string type, string message)
        {
            _adminNotificationManager.TAdd(new AdminNotification
            {
                Type = type,
                Message = message,
                CreatedTime = DateTime.Now
            });
        }
    }
}
