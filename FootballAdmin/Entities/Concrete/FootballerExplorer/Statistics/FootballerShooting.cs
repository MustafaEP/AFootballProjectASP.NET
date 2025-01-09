using Entities.Concrete.FootballerStatistics.BaseStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics.Statistics
{
    public class FootballerShooting : BaseStatistic
    {
        public int Positioning { get; set; }
        public int Finishing { get; set; }
        public int ShotPower { get; set; }
        public int LongShots { get; set; }
        public int Volleys { get; set; }
        public int Penalties { get; set; }
    }
}
