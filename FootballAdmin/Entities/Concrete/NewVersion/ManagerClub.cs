using Entities.Concrete.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.NewVersion
{
    public class ManagerClub : BasePlus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public string LineUp { get; set; }
        public List<ClubsFootballer>? Footballers { get; set; }
    }
}
