using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics
{
    public class FootballerAbility
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FootballerStatistic")]
        public int FootballerStatisticId { get; set; } // Hangi futbolcuya ait olduğunu belirtir

        public string AbilityName { get; set; } // Örneğin: "Top Kontrolü", "Frikik Atışı"
        public string Description { get; set; } // Özel yeteneğin açıklaması (isteğe bağlı)


        public FootballerStatistic FootballerStatistic { get; set; }
    }
}
