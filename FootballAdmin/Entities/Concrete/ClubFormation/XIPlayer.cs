using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ClubFormation
{
    public class XIPlayer
    {
        [Key]
        public int Id { get; set; }


        public int FormationId {  get; set; }
        public Formation Formation { get; set; }


        public int? AddedFootballerId { get; set; }
        public AddedFootballer? AddedFootballer { get; set; }
        public string Position { get; set; }
    }
}
