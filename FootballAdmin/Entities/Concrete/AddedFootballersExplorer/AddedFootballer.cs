using Entities.Concrete.AddedFootballersExplorer;
using Entities.Concrete.ClubFormation;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.AddedFootballersExplorer
{
    public class AddedFootballer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Footballer")]
        public int FootballerId { get; set; }
        public Footballer Footballer { get; set; }

        [ForeignKey("ManagerClub")]
        public int ClubId { get; set; }
        public ManagerClub Club { get; set; }


        public AddedFootballerUpgrade Upgrade { get; set; }

        public DateTime DateAdded { get; set; } 

        
    }


}
