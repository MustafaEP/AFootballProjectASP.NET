using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class ScoreofFootballer : BasePlus
    {
        public int Id { get; set; }
        public int Goal { get; set; }
        public int Asist { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public float GoalExpectetion { get; set; }


        [ForeignKey("ClubsFootballer")]
        public int ClubsFootballerId { get; set; }
        public ClubsFootballer ClubsFootballer { get; set; } 
    }
}
