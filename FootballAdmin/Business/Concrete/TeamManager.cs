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
    public class TeamManager : ITeamService
    {
        ITeamDal _teamDal;

        public TeamManager(ITeamDal teamDal)
        {
            _teamDal = teamDal;
        }

        public List<Team> GetList()
        {
            return _teamDal.GetListAll();
        }

        public Team TGetById(int id)
        {
            return _teamDal.GetById(id);
        }

        public void TAdd(Team entity)
        {
            _teamDal.Insert(entity);
        }

        public void TDelete(Team entity)
        {
            _teamDal.Delete(entity);
        }


        public void TUpdate(Team entity)
        {
            _teamDal.Update(entity);
        }

        public int TCount()
        {
            return _teamDal.GetListAll().Count();
        }
    }
}
