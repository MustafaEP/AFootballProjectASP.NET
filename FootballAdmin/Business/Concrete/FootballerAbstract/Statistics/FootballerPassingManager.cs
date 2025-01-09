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
    public class FootballerPassingManager : IFootballerPassingService
    {
        IFootballerPassingDal _dal;

        public FootballerPassingManager(IFootballerPassingDal dal)
        {
            _dal = dal;
        }
        public FootballerPassing GetStatistic(int statisticId)
        {
            return _dal.GetListAll().FirstOrDefault(x => x.StatisticsId == statisticId);
        }
        public List<FootballerPassing> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerPassing entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerPassing entity)
        {
            throw new NotImplementedException();
        }

        public FootballerPassing TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerPassing entity)
        {
            throw new NotImplementedException();
        }
    }
}
