using Core.Access.Abstract.GenericDal;
using Entities.Concrete.FootballerExplorer;
using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.FootballerAbstract
{
    public interface IFootballerDataDal : IGenericDal<FootballerData>
    {
    }
}
