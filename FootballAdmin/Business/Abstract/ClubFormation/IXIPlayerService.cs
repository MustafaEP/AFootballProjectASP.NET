using Business.Abstract.GenericService;
using Entities.Concrete.ClubFormation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.ClubFormation
{
    public interface IXIPlayerService : IGenericService<XIPlayer>
    {
        List<XIPlayer> GetFormationPlayer(int formationId);
        XIPlayer GetXIPlayerinFormation(int formationId, int? addedFootballerId);
        void UpdateXI(List<XIPlayer> xIPlayers);
    }
}
