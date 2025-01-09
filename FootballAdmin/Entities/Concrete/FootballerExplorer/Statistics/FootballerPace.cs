using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.FootballerStatistics.BaseStatistics;

namespace Entities.Concrete.FootballerStatistics.Statistics
{
    public class FootballerPace : BaseStatistic
    {
        public int Acceleration { get; set; }
        public int SprintSpeed { get; set; }
    }
}
