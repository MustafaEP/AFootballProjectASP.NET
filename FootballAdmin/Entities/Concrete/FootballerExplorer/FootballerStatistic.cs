using Entities.Concrete.FootballerExplorer;
using Entities.Concrete.FootballerStatistics.Statistics;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete.FootballerStatistics
{
    public class FootballerStatistic
    {
        public int Id { get; set; }

        [ForeignKey("Footballer")]
        public int FootballerId { get; set; }


        public int Pace => CalculateAverage(PaceDetails);
        public int Shooting => CalculateAverage(ShootingDetails);
        public int Passing => CalculateAverage(PassingDetails);
        public int Dribbling => CalculateAverage(DribblingDetails);
        public int Defending => CalculateAverage(DefendingDetails);
        public int Physicality => CalculateAverage(PhysicalityDetails);

        public int Power => CalculetePower(Pace, Shooting, Passing, Dribbling, Defending, Physicality, GetMainPosition());


        [JsonIgnore]
        public Footballer Footballer { get; set; }
        public ICollection<FootballerAbility> footballerAbilities { get; set; }
        
        
        public FootballerData FootballerDatas { get; set; }

        public FootballerPace PaceDetails { get; set; }
        public FootballerShooting ShootingDetails { get; set; }
        public FootballerPassing PassingDetails { get; set; }
        public FootballerDribbling DribblingDetails { get; set; }
        public FootballerDefending DefendingDetails { get; set; }
        public FootballerPhysicality PhysicalityDetails {  get; set; }
    
        
        private int CalculateAverage(object details)
        {
            if (details == null) 
                return 0;

            var properties = details.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(int) &&
                    p.Name != "Id" &&
                    p.Name != "StatisticsId")
                .Select(p => (int)p.GetValue(details))
                .ToList();

            return properties.Any() ? (int)properties.Average() : 0;
        }


        private int CalculetePower(int pace, int shooting, int passing, int dribbling, int defending, int physicality, string position)
        {
            var answer = 0;
            switch (position)
            {
                case "SNT":
                    answer += pace * 25;
                    answer += shooting * 40;
                    answer += passing * 10;
                    answer += dribbling * 20;
                    answer += defending * 5;
                    answer += physicality * 20;
                    break;

                case "SLK":
                case "SĞK":
                    answer += pace * 35;
                    answer += shooting * 25;
                    answer += passing * 20;
                    answer += dribbling * 25;
                    answer += defending * 5;
                    answer += physicality * 10;
                    break;

                case "SLO":
                case "SĞO":
                    answer += pace * 25;
                    answer += shooting * 20;
                    answer += passing * 25;
                    answer += dribbling * 25;
                    answer += defending * 15;
                    answer += physicality * 10;
                    break;

                case "MOO":
                    answer += pace * 20;
                    answer += shooting * 20;
                    answer += passing * 25;
                    answer += dribbling * 20;
                    answer += defending * 15;
                    answer += physicality * 20;
                    break;

                case "MO":
                    answer += pace * 10;
                    answer += shooting * 10;
                    answer += passing * 35;
                    answer += dribbling * 20;
                    answer += defending * 25;
                    answer += physicality * 20;
                    break;

                case "MDO":
                    answer += pace * 10;
                    answer += shooting * 10;
                    answer += passing * 30;
                    answer += dribbling * 10;
                    answer += defending * 30;
                    answer += physicality * 30;
                    break;

                case "SĞB":
                case "SLB":
                    answer += pace * 25;
                    answer += shooting * 5;
                    answer += passing * 20;
                    answer += dribbling * 20;
                    answer += defending * 20;
                    answer += physicality * 30;
                    break;
                
                case "STP":
                    answer += pace * 15;
                    answer += shooting * 5;
                    answer += passing * 15;
                    answer += dribbling * 10;
                    answer += defending * 40;
                    answer += physicality * 35;
                    break;

                case "KL":
                    answer += pace * 20;
                    answer += shooting * 20;
                    answer += passing * 20;
                    answer += dribbling * 20;
                    answer += defending * 20;
                    answer += physicality * 20;
                    answer += 3500;
                    break;


                default:
                    break;
            }

            return answer / 100;
        }

        private string GetMainPosition()
        {
            return Footballer?.FootballerPositions?.FirstOrDefault().ShortPosition;
        }
    }
}
