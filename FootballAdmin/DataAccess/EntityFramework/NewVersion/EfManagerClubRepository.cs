using Access.Abstract.NewVersion;
using Access.EntityFramework.GenericRepository;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.NewVersion
{
    public class EfManagerClubRepository : GenericRepository<ManagerClub>, IManagerClubDal
    {
    }
}
