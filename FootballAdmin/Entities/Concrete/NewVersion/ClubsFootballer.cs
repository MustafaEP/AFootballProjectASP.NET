using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class ClubsFootballer : BasePlus
    {
        [Key] //Buralar Çok Değişecek......
        public int Id { get; set; }
        public string username { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Country { get; set; }
        public string Position { get; set; }
        public string AltPositions { get; set; } //Pozisyonlar tablosu oluşturulacak
        public string PreferredFoot { get; set; }


        //Statistics -- İstatistiler
        public int Pace { get; set; } //Detaylandırılacak...
        public int Shooting { get; set; }//Detaylandırılacak...
        public int Passing { get; set; }//Detaylandırılacak...
        public int Dribbling { get; set; }//Detaylandırılacak...
        public int Defending { get; set; }//Detaylandırılacak...
        public int Physicality { get; set; }//Detaylandırılacak...


        public string Abilities { get; set; } //Detaylandırılacak
        public string ClubName { get; set; }
        public int PaceUpgrade { get; set; }
        public int ShootingUpgrade { get; set; }
        public int PassingUpgrade { get; set; }
        public int DribblingUpgrade { get; set; }
        public int DefendingUpgrade { get; set; }
        public int PhysicalityUpgrade { get; set; }
        public int Confidence { get; set; } //Güven

        public int ManagerClubId {  get; set; }


        public ScoreofFootballer? ScoreofFootballer { get; set; }
    }
}
