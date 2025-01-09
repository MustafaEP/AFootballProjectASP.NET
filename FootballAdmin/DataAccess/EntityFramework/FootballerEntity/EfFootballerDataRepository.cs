using Access.Abstract.FootballerAbstract;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete.FootballerExplorer;
using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.FootballerEntity
{
    public class EfFootballerDataRepository : GenericRepository<FootballerData>, IFootballerDataDal
    {
    }
}
