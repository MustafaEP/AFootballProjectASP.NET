
using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics.Statistics;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.FootballerAbstract.Statistics
{
    public interface IFootballerDefendingService : IGenericService<FootballerDefending>
    {
        FootballerDefending GetStatistic(int statisticId);
    }
}
