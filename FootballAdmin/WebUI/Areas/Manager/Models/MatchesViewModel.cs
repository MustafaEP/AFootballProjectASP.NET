using Entities.Concrete.Matches;

namespace WebUI.Areas.Manager.Models
{
    public class MatchesViewModel
	{
		public string HomeTeamManagerName { get; set; }
		public string HomeTeamManagerAvatar { get; set; }
		public string AwayTeamManagerName { get; set; }
		public string AwayTeamManagerAvatar { get; set; }
		public FootballMatch FootballMatch { get; set; }
		public bool? isHomeClub { get; set; }
		public int? homeWinning {  get; set; }
		public int? awayWinning { get; set; }

	}
}
