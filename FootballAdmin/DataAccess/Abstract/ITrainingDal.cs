﻿using Core.Access.Abstract.GenericDal;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract
{
    public interface ITrainingDal : IGenericDal<TrainingSession>
    {
    }
}
