using Access.Abstract.NewVersion;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.NewVersion
{
    public class EfFootballMatchRepository : GenericRepository<FootballMatch>, IFootballMatchDal
    {
    }
}
