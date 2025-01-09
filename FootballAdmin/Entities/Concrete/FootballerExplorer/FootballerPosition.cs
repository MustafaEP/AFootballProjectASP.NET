using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.NewVersion;

namespace Entities.Concrete.FootballerStatistics
{
    public class FootballerPosition
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Footballer")]
        public int FootballerId { get; set; } 

        public string ShortPosition {  get; set; }
        public string? Position { get; set; } 
        public string? Description { get; set; } 


        public Footballer Footballer { get; set; }
    }
}
