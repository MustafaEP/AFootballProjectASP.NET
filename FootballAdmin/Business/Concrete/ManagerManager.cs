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
    public class ManagerManager : IManagerService
    {
        IManagerDal _managerDal;

        public ManagerManager(IManagerDal managerDal)
        {
            _managerDal = managerDal;
        }

        public Manager Login(string username, string password)
        {
            var user = _managerDal.GetListAll().FirstOrDefault(x => x.UserName == username && x.Password == password);
            return user;
        }

        public List<Manager> GetList()
        {
            return _managerDal.GetListAll();
        }

        public void TAdd(Manager entity)
        {
            _managerDal.Insert(entity);
        }

        public int TCount()
        {
            return _managerDal.GetListAll().Count();
        }

        public void TDelete(Manager entity)
        {
            _managerDal.Delete(entity);
        }

        public Manager TGetById(int id)
        {
            return _managerDal.GetById(id);
        }

        public void TUpdate(Manager entity)
        {
            _managerDal.Update(entity);
        }
    }
}
