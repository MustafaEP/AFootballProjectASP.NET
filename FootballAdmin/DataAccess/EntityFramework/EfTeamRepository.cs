using Access.Abstract;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework
{
    public class EfTeamRepository  :GenericRepository<Team>, ITeamDal 
    {
    }
}
