using Access.Abstract.FootballerAbstract.Statistics;
using Business.Abstract.FootballerAbstract;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.FootballerAbstract
{
    public class FootballerStatisticManager : IFootballerStatisticService
    {
        IFootballerStatisticDal _dal;

        public FootballerStatisticManager(IFootballerStatisticDal dal)
        {
            _dal = dal;
        }

        public List<FootballerStatistic> GetList()
        {
            throw new NotImplementedException();
        }

        public FootballerStatistic GetStatistic(int footballerId)
        {
            return _dal.GetListAll().FirstOrDefault(x => x.FootballerId == footballerId);
        }

        public FootballerStatistic GetStatistics(int footballerId)
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerStatistic entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerStatistic entity)
        {
            throw new NotImplementedException();
        }

        public FootballerStatistic TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerStatistic entity)
        {
            throw new NotImplementedException();
        }
    }
}
