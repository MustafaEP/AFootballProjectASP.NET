using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics.BaseStatistics
{
    public class BaseStatistic
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FootballerStatistic")]
        public int StatisticsId { get; set; }
        public FootballerStatistic FootballerStatistic { get; set; }
    }
}
