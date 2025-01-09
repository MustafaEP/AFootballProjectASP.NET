using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ClubFormation
{
    public class Formation
    {
        [Key]
        public int Id { get; set; }
        public int ManagerClubId { get; set; }
        public ManagerClub ManagerClub { get; set; }
        public string FotmationType { get; set; }
        public List<XIPlayer> XIPlayers { get; set; }
    }
}
