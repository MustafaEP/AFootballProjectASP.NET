using Business.Abstract.GenericService;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.NewVersion
{
    public interface IScoreofFootballerService : IGenericService<ScoreofFootballer>
    {
        public ScoreofFootballer GetScoresFootballer(int footbollerId);
    }
}
