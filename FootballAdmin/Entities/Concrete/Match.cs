using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Match : BasePlus
    {
        public int Id { get; set; }
        public DateTime? MatchDate { get; set; }
        public string? Result { get; set; }  

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId {  get; set; }
        public Team AwayTeam { get; set; }
    }
}
