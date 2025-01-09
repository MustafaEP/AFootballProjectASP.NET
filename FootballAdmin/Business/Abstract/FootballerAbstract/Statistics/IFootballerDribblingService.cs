
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
    public interface IFootballerDribblingService : IGenericService<FootballerDribbling>
    {
        FootballerDribbling GetStatistic(int statisticId);
    }
}
