using Access.Abstract;
using Access.Abstract.AddedFootballers;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.AddedFootballers
{
    public class EfAddedFootballerRepository : GenericRepository<AddedFootballer>, IAddedFootballerDal
    {
    }
}
