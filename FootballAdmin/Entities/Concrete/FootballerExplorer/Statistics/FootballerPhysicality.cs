using Entities.Concrete.FootballerStatistics.BaseStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics.Statistics
{
    public class FootballerPhysicality : BaseStatistic
    {
        public int Jumping { get; set; }
        public int Stamina { get; set; }
        public int Strength { get; set; }
        public int Aggression { get; set; }
    }
}
