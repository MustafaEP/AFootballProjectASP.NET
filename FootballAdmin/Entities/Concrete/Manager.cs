using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Generics;

namespace Entities.Concrete
{
    public class Manager : Base
    {
        public string PreferredLineUp {  get; set; }
        public int? TeamId { get; set; }
    }
}
