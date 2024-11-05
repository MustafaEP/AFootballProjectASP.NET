using Access.Abstract;
using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminNotificationManager : IAdminNotificationService
    {
        IAdminNotificationDal _notificationDal;

        public AdminNotificationManager(IAdminNotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public List<AdminNotification> GetList()
        {
            return _notificationDal.GetListAll();
        }

        public void TAdd(AdminNotification entity)
        {
            _notificationDal.Insert(entity);
        }

        public int TCount()
        {
            return _notificationDal.GetListAll().Count();
        }

        public void TDelete(AdminNotification entity)
        {
            _notificationDal.Delete(entity);
        }

        public AdminNotification TGetById(int id)
        {
            return _notificationDal.GetById(id);
        }

        public void TUpdate(AdminNotification entity)
        {
            _notificationDal.Update(entity);
        }
    }
}
