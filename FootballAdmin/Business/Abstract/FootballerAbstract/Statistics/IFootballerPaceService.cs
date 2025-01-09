
using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.FootballerAbstract.Statistics
{
    public interface IFootballerPaceService : IGenericService<FootballerPace>
    {
        FootballerPace GetStatistic(int statisticId);
    }
}
