using Access.Abstract.FootballerAbstract.Statistics;
using Access.EntityFramework.FootballerEntity;
using Business.Abstract.FootballerAbstract;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics;
using Entities.Concrete.FootballerStatistics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.FootballerAbstract
{
    public class FootballerPositionManager : IFootballerPositionService
    {
        IFootballerPositionDal _dal;

        public FootballerPositionManager(IFootballerPositionDal dal)
        {
            _dal = dal;
        }

        public List<FootballerPosition> GetList()
        {
            throw new NotImplementedException();
        }

        public List<FootballerPosition> GetPositions(int footballerId)
        {
            return _dal.GetListAll().Where(x => x.FootballerId == footballerId).ToList();
        }

        public void TAdd(FootballerPosition entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerPosition entity)
        {
            throw new NotImplementedException();
        }

        public FootballerPosition TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerPosition entity)
        {
            throw new NotImplementedException();
        }
    }
}
