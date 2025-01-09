using Core.Access.Abstract.GenericDal;
using Entities.Concrete;
using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.AddedFootballers
{
    public interface IAddedFootballerDal : IGenericDal<AddedFootballer>
    {
    }
}
