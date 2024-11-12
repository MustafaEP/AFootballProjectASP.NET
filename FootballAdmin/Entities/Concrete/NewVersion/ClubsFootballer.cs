using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class ClubsFootballer : BasePlus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Country { get; set; }
        public string Position { get; set; }
        public string AltPositions { get; set; }
        public string PreferredFoot { get; set; }


        //Statistics -- İstatistiler
        public int Pace { get; set; }
        public int Shooting { get; set; }
        public int Passing { get; set; }
        public int Dribbling { get; set; }
        public int Defending { get; set; }
        public int Physicality { get; set; }


        public string Abilities { get; set; }
        public string ClubName { get; set; }
        public string PaceUpgrade { get; set; }
        public string ShootingUpgrade { get; set; }
        public string PassingUpgrade { get; set; }
        public string DribblingUpgrade { get; set; }
        public string DefendingUpgrade { get; set; }
        public string PhysicalityUpgrade { get; set; }
        public string Confidence {  get; set; }


        public int ManagerClubId {  get; set; }
    }
}
