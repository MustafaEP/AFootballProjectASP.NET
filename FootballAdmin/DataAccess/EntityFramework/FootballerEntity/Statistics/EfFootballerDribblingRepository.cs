using Access.Abstract;
using Access.Abstract.FootballerAbstract.Statistics;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.FootballerEntity.Statistics
{
    public class EfFootballerDribblingRepository : GenericRepository<FootballerDribbling>, IFootballerDribblingDal
    {
    }
}
