using Access.Abstract.NewVersion;
using Business.Abstract.NewVersion;
using Entities.Concrete.Matches;

namespace Business.Concrete.NewVersion
{
    public class FootbalMatchManager : IFootballMatchService
    {
        IFootballMatchDal _footballMatchDal;

        public FootbalMatchManager(IFootballMatchDal footballMatchDal)
        {
            _footballMatchDal = footballMatchDal;
        }

        public List<FootballMatch> GetAcceptedClubMatches(int clubId)
        {
            return _footballMatchDal.GetListAll()
                .Where(match => (match.AwayClubId == clubId && match.isAccepted == true) || (match.HomeClubId == clubId && match.isAccepted == true))
                .OrderByDescending(match => match.CreatedTime)
                .Take(5)
                .ToList();
        }

        public List<FootballMatch> GetClubMatches(int clubId)
        {
            return _footballMatchDal.GetListAll()
                .Where(match => (match.AwayClubId == clubId) || (match.HomeClubId == clubId))
				.OrderByDescending(match => match.CreatedTime)
				.ToList();
        }

        public List<FootballMatch> GetList()
        {
            return _footballMatchDal.GetListAll();
        }

        public List<FootballMatch> GetPlayed()
        {
            return _footballMatchDal.GetListAll().Where(x => x.isAccepted == true).ToList();
        }

        public void TAdd(FootballMatch entity)
        {
            _footballMatchDal.Insert(entity);
        }

        public int TCount()
        {
            return _footballMatchDal.GetListAll().Count();
        }

        public void TDelete(FootballMatch entity)
        {
            _footballMatchDal.Delete(entity);
        }

        public FootballMatch TGetById(int id)
        {
            return _footballMatchDal.GetById(id);
        }

        public void TUpdate(FootballMatch entity)
        {
            _footballMatchDal.Update(entity);
        }
    }
}
