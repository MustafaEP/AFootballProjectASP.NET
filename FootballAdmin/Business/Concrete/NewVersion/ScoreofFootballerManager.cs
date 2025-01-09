using Access.Abstract.NewVersion;
using Business.Abstract.NewVersion;
using Entities.Concrete.NewVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.NewVersion
{
    public class ScoreofFootballerManager : IScoreofFootballerService
    {
        IScoreofFootballerDal _scoreofFootballerDal;

        public ScoreofFootballerManager(IScoreofFootballerDal scoreofFootballerDal)
        {
            _scoreofFootballerDal = scoreofFootballerDal;
        }

        public List<ScoreofFootballer> GetList()
        {
            return _scoreofFootballerDal.GetListAll();
        }

        public ScoreofFootballer GetScoresFootballer(int footbollerId)
        {
            return _scoreofFootballerDal.GetListAll().FirstOrDefault(x => x.ClubsFootballerId == footbollerId);
        }

        public void TAdd(ScoreofFootballer entity)
        {
            _scoreofFootballerDal.Insert(entity);
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(ScoreofFootballer entity)
        {
            _scoreofFootballerDal.Delete(entity);
        }

        public ScoreofFootballer TGetById(int id)
        {
            return _scoreofFootballerDal.GetById(id);
        }

        public void TUpdate(ScoreofFootballer entity)
        {
            _scoreofFootballerDal.Update(entity);
        }
    }
}
