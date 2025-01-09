using Access.Abstract.NewVersion;
using Access.Context;
using Core.Access.EntityFramework.GenericRepository;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework.NewVersion
{
    public class EfFootballerRepository : GenericRepository<Footballer>, IFootballerDal
    {
        public IQueryable<Footballer> GetListEveryThink(Expression<Func<Footballer, bool>> filter = null)
        {
            using var _context = new Conn();
            return filter == null
                ? _context.Footballers.AsQueryable()
                : _context.Footballers.Where(filter);
        }
    }
}
