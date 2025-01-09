using Access.Abstract;
using Access.Abstract.FootballerAbstract;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.FootballerEntity
{
    public class EfFootballerStatisticRepository : GenericRepository<FootballerStatistic>, IFootballerStatisticDal
    {
    }
}
