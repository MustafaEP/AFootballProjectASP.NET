using Core.Access.Abstract.GenericDal;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.NewVersion
{
    public interface IFootballerDal : IGenericDal<Footballer>
    {
        IQueryable<Footballer> GetListEveryThink(Expression<Func<Footballer, bool>> filter = null);
    }
}
