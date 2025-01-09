using Access.Abstract.FootballerAbstract.Statistics;
using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.FootballerAbstract
{
    public class FootballerShootingManager : IFootballerShootingService
    {
        IFootballerShootingDal _dal;

        public FootballerShootingManager(IFootballerShootingDal dal)
        {
            _dal = dal;
        }
        public FootballerShooting GetStatistic(int statisticId)
        {
            return _dal.GetListAll().FirstOrDefault(x => x.StatisticsId == statisticId);
        }
        public List<FootballerShooting> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerShooting entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerShooting entity)
        {
            throw new NotImplementedException();
        }

        public FootballerShooting TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerShooting entity)
        {
            throw new NotImplementedException();
        }
    }
}
