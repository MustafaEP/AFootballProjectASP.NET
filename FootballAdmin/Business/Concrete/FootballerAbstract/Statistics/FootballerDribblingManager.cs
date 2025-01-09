using Access.Abstract.AddedFootballers;
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
    public class FootballerDribblingManager : IFootballerDribblingService
    {
        IFootballerDribblingDal _footballerDribblingDal;

        public FootballerDribblingManager(IFootballerDribblingDal footballerDribblingDal)
        {
            _footballerDribblingDal = footballerDribblingDal;
        }

        public FootballerDribbling GetStatistic(int statisticId)
        {
            return _footballerDribblingDal.GetListAll().FirstOrDefault(x => x.StatisticsId == statisticId);
        }

        public List<FootballerDribbling> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerDribbling entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerDribbling entity)
        {
            throw new NotImplementedException();
        }

        public FootballerDribbling TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerDribbling entity)
        {
            throw new NotImplementedException();
        }
    }
}
