using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.AddedFootballersExplorer
{
    public class AddedFootballerUpgrade
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AddedFootballer")]
        public int AddedFootballerId { get; set; }
        public AddedFootballer AddedFootballer { get; set; }

        public DateTime UpgradeDate { get; set; } 

        
        public int PaceChange { get; set; } 
        public int ShootingChange { get; set; }
        public int PassingChange { get; set; } 
        public int DribblingChange { get; set; } 
        public int DefendingChange { get; set; } 
        public int PhysicalityChange { get; set; } 
        public int Confidence {  get; set; }
    }

}
