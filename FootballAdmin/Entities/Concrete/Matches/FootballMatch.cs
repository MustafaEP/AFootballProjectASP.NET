using Entities.Concrete.Generics;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Matches
{
    public class FootballMatch : BasePlus
    {
        public int Id { get; set; }
        public bool status { get; set; }
        public bool isAccepted { get; set; }
        public bool isRejected { get; set; }
        public int HomeClubId { get; set; }
        public ManagerClub? HomeClub { get; set; }
        public int AwayClubId { get; set; }
        public ManagerClub? AwayClub { get; set; }
        public string MatchString { get; set; }
        public int HomeClubGoal { get; set; }
        public int AwayClubGoal { get; set; }
        public string Goals { get; set; }
        public string Asists { get; set; }
        public string HomeRedCards { get; set; }
        public string AwayRedCards { get; set; }
        public string HomeYellowCards { get; set; }
        public string AwayYellowCards { get; set; }
        public int HomeCorners { get; set; }
        public int AwayCorners { get; set; }
        public int HomeSetPlay { get; set; }
        public int AwaySetPlay { get; set; }
        public int HomeBallPlaying { get; set; }
        public int AwayBallPlaying { get; set; }
        public string ProposeLetter { get; set; }
    }
}
