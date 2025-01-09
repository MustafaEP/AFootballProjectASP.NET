using Entities.Concrete.AddedFootballersExplorer;
using Entities.Concrete.ClubFormation;
using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class ManagerClub : BasePlus
    {
        [Key]
        public int Id { get; set; }
        public string? Avatar { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public string LineUp { get; set; }
        public int? Budget { get; set; }
        public List<ClubsFootballer>? Footballers { get; set; }

        public Formation Formation { get; set; }

        public ICollection<AddedFootballer> AddedFootballers { get; set; }
    }
}
