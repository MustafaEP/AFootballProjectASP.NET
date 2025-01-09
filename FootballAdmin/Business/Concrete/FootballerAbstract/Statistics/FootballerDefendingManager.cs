using Access.Abstract.AddedFootballers;
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
    public class FootballerDefendingManager : IFootballerDefendingService
    {
        IFootballerDefendingDal _footballerDefendingDal;

        public FootballerDefendingManager(IFootballerDefendingDal footballerDefendingDal)
        {
            _footballerDefendingDal = footballerDefendingDal;
        }

        public List<FootballerDefending> GetList()
        {
            throw new NotImplementedException();
        }

        public FootballerDefending GetStatistic(int statisticId)
        {
            return _footballerDefendingDal.GetListAll().FirstOrDefault(x => x.StatisticsId == statisticId);
        }

        public void TAdd(FootballerDefending entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerDefending entity)
        {
            throw new NotImplementedException();
        }

        public FootballerDefending TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerDefending entity)
        {
            throw new NotImplementedException();
        }
    }
}
