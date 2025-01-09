using Entities.Concrete.AddedFootballersExplorer;
using Entities.Concrete.FootballerStatistics;
using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class Footballer : BasePlus
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; } 
        public string Name { get; set; }
        public string SurName { get; set; }
        public int MarketPrice { get; set; }
        public string? Country { get; set; }

        public FootballerStatistic Statistic { get; set; }
        public ICollection<FootballerPosition>  FootballerPositions { get; set; }


        public ICollection<AddedFootballer> AddedFootballers { get; set; }



        //Kullanım Dışı!!!

        
        public string? Position { get; set; }
        public string? AltPositions { get; set; }
        public string? PreferredFoot { get; set; }

        public string? Abilities { get; set; }
        public string? ClubName { get; set; }




        //Statistics -- İstatistiler
        public int? Pace { get; set; }
        public int? Shooting { get; set; }
        public int? Passing { get; set; }
        public int? Dribbling { get; set; }
        public int? Defending { get; set; }
        public int? Physicality { get; set; }
    }
}
