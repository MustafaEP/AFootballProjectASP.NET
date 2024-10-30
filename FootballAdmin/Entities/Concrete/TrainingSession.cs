using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{

    public class TrainingSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

    }

}
