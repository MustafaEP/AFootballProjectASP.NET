using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public List<Player> ListPlayer { get; set; }
        public int? ManagerId { get; set; }
        public Manager? Manager { get; set; }

        // Takımın ev sahibi olarak oynadığı maçlar
        public ICollection<Match> HomeMatches { get; set; }

        // Takımın deplasman takımı olarak oynadığı maçlar
        public ICollection<Match> AwayMatches { get; set; }
    }
}
