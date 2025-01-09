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
    public class FootballerPaceManager : IFootballerPaceService
    {
        IFootballerPaceDal _dal;

        public FootballerPaceManager(IFootballerPaceDal dal)
        {
            _dal = dal;
        }

        public FootballerPace GetStatistic(int statisticId)
        {
            return _dal.GetListAll().FirstOrDefault(x => x.StatisticsId == statisticId);
        }

        public List<FootballerPace> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerPace entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerPace entity)
        {
            throw new NotImplementedException();
        }

        public FootballerPace TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerPace entity)
        {
            throw new NotImplementedException();
        }
    }
}
