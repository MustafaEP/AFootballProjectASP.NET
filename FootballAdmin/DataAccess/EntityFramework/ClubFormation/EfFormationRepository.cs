using Access.Abstract;
using Access.Abstract.ClubFormation;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete;
using Entities.Concrete.ClubFormation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.ClubFormation
{
    public class EfFormationRepository : GenericRepository<Formation>,IFormationDal
    {
    }
}
