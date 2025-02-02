﻿using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.FootballerAbstract
{
    public interface IFootballerPositionService : IGenericService<FootballerPosition>
    {
       List<FootballerPosition> GetPositions(int footballerId);
    }
}
