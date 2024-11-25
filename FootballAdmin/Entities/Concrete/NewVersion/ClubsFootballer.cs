using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class ClubsFootballer : BasePlus
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
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
        public int PaceUpgrade { get; set; }
        public int ShootingUpgrade { get; set; }
        public int PassingUpgrade { get; set; }
        public int DribblingUpgrade { get; set; }
        public int DefendingUpgrade { get; set; }
        public int PhysicalityUpgrade { get; set; }
        public int Confidence { get; set; } //Güven


        public int ManagerClubId {  get; set; }
    }
}
