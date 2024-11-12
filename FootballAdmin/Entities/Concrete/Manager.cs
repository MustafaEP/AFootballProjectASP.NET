using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Generics;
using Entities.Concrete.NewVersion;

namespace Entities.Concrete
{
    public class Manager : Base
    {
        public string PreferredLineUp {  get; set; }
        public int? TeamId { get; set; }


        [ForeignKey("ManagerClub")]
        public int? ManagerClubId {  get; set; }
        public ManagerClub? ManagerClub { get; set; }
    }
}
