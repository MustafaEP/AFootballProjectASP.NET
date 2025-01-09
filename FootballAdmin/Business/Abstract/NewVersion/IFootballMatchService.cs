using Business.Abstract.GenericService;
using Entities.Concrete.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.NewVersion
{
    public interface IFootballMatchService : IGenericService<FootballMatch>
    {
        List<FootballMatch> GetAcceptedClubMatches(int clubId);
        List<FootballMatch> GetClubMatches(int clubId);
        List<FootballMatch> GetPlayed();
    }
}
