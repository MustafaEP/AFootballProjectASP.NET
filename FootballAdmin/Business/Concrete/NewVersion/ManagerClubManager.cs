using Access.Abstract;
using Access.Abstract.NewVersion;
using Business.Abstract.NewVersion;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.NewVersion
{
    public class ManagerClubManager : IManagerClubService
    {

        IManagerClubDal _managerClubDal;

        public ManagerClubManager(IManagerClubDal managerClubDal)
        {
            _managerClubDal = managerClubDal;
        }

        public List<ManagerClub> GetList()
        {
            return _managerClubDal.GetListAll();
        }

        public ManagerClub OwnClub(int managerId)
        {
            return _managerClubDal.GetListAll().Where(x => x.ManagerId == managerId).FirstOrDefault();
        }

        public void TAdd(ManagerClub entity)
        {
            _managerClubDal.Insert(entity);
        }

        public int TCount()
        {
            return _managerClubDal.GetListAll().Count();
        }

        public void TDelete(ManagerClub entity)
        {
            _managerClubDal.Delete(entity);
        }

        public ManagerClub TGetById(int id)
        {
            return _managerClubDal.GetById(id);
        }

        public void TUpdate(ManagerClub entity)
        {
            _managerClubDal.Update(entity);
        }
    }
}
