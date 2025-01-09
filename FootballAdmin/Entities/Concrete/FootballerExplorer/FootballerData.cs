using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerExplorer
{
    public class FootballerData
    {
        public int Id { get; set; }

        [ForeignKey("FootballerStatistic")]
        public int StatisticsId { get; set; }
        public int Age {  get; set; }
        public string Weght {  get; set; }
        public string Height { get; set; }
        public string Foot {  get; set; }

        public FootballerStatistic FootballerStatistic { get; set; }

    }
}
