using Entities.Concrete.FootballerStatistics.BaseStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics.Statistics
{
    public class FootballerDribbling : BaseStatistic
    {
        public int Agility { get; set; } //Çeviklik
        public int Balance { get; set; } //Denge
        public int Reactions { get; set; }
        public int BallControl { get; set; }
        public int DribblingP { get; set; }
        public int Composure { get; set; } //Soğukkanlılık
    }
}
