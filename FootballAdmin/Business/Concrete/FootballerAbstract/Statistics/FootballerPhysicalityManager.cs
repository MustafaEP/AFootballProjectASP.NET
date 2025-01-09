using Business.Abstract.FootballerAbstract.Statistics;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.FootballerAbstract.Statistics
{
    public class FootballerPhysicalityManager : IFootballerPhysicalityService
    {
        IFootballerPhysicalityDal _dal;

        public FootballerPhysicalityManager(IFootballerPhysicalityDal dal)
        {
            _dal = dal;
        }
        public FootballerPhysicality GetStatistic(int statisticId)
        {
            return _dal.GetListAll().FirstOrDefault(x => x.StatisticsId == statisticId);
        }
        public List<FootballerPhysicality> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerPhysicality entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerPhysicality entity)
        {
            throw new NotImplementedException();
        }

        public FootballerPhysicality TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerPhysicality entity)
        {
            throw new NotImplementedException();
        }
    }
}
