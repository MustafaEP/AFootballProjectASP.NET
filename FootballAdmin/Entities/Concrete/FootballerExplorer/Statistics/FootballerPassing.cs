using Entities.Concrete.FootballerStatistics.BaseStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics.Statistics
{
    public class FootballerPassing : BaseStatistic
    {
        public int Vision { get; set; }
        public int Crossing { get; set; } //Orta Açma
        public int FreeKickAccuracy { get; set; }
        public int ShortPassing { get; set; }
        public int LongPassing { get; set; }
        public int Curve { get; set; } // Falso

    }
}
