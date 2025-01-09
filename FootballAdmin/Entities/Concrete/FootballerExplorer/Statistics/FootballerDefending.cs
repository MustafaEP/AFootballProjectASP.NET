using Entities.Concrete.FootballerStatistics.BaseStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics.Statistics
{
    public class FootballerDefending : BaseStatistic
    {
        public int Interceptions { get; set; } //Top Kesme
        public int HeadingAccuracy { get; set; } //Kafa İsabeti
        public int DefAwareness { get; set; } //Defansif Farkındalık
        public int StandingTackle { get; set; } //Ayakta Müdahele
        public int SlidingTackle { get; set; } //Kayarak Müdahele
    }
}
