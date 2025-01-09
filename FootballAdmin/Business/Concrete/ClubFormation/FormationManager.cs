using Access.Abstract.ClubFormation;
using Access.Abstract.NewVersion;
using Business.Abstract.ClubFormation;
using Entities.Concrete.ClubFormation;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.ClubFormation
{
    public class FormationManager : IFormationService
    {
        IFormationDal _dal;

        public FormationManager(IFormationDal dal)
        {
            _dal = dal;
        }

        

        public Formation GetByClubId(int managerClubId)
        {
            return _dal.GetListAll().FirstOrDefault(x => x.ManagerClubId == managerClubId);
        }

        public List<Formation> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Formation entity)
        {
            _dal.Insert(entity);
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(Formation entity)
        {
            _dal.Delete(entity);
        }

        public Formation TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Formation entity)
        {
            _dal.Update(entity);
        }
    }
}
