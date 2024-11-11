using Access.Abstract;
using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public List<Admin> GetList()
        {
            throw new NotImplementedException();
        }

        public Admin Login(string username, string password)
        {
            var user = _adminDal.GetListAll().FirstOrDefault(x => x.UserName == username && x.Password == password);
            return user;
        }

        public void TAdd(Admin entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(Admin entity)
        {
            throw new NotImplementedException();
        }

        public Admin TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Admin entity)
        {
            throw new NotImplementedException();
        }
    }
}
