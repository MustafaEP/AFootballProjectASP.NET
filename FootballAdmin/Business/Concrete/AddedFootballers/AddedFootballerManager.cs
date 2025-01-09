using Access.Abstract;
using Access.Abstract.AddedFootballers;
using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.AddedFootballers
{
    public class AddedFootballerManager : IAddedFootballerService
    {
        IAddedFootballerDal _addedFootballerDal;

        public AddedFootballerManager(IAddedFootballerDal addedFootballerDal)
        {
            _addedFootballerDal = addedFootballerDal;
        }

        public AddedFootballer GetAddedFootballer(int clubId, int footballerId)
        {
            return _addedFootballerDal.GetListAll().FirstOrDefault(x => x.ClubId == clubId && x.FootballerId == footballerId);
        }

        public List<AddedFootballer> GetAddedsClub(int clubId)
        {
            return _addedFootballerDal.GetListAll().Where(x => x.ClubId == clubId).ToList();
        }

        public List<AddedFootballer> GetList()
        {
            return _addedFootballerDal.GetListAll();
        }

        public bool isAlone(int clubId, int footballerId)
        {
            return GetList().FirstOrDefault
                (x => (x.ClubId == clubId) && (x.FootballerId == footballerId)) 
                != null ? true : false;
        }

        public void TAdd(AddedFootballer entity)
        {
            _addedFootballerDal.Insert(entity);
        }

        public int TCount()
        {
            return _addedFootballerDal.GetListAll().Count();
        }

        public void TDelete(AddedFootballer entity)
        {
            _addedFootballerDal.Delete(entity);
        }

        public AddedFootballer TGetById(int id)
        {
            return _addedFootballerDal.GetById(id);
        }

        public AddedFootballer GetByIdWithFootballer(int addedFootballerId)
        {
            return _addedFootballerDal.GetWithInclude(
                af => af.Id == addedFootballerId,
                af => af.Footballer
                );
        }

        public AddedFootballer GetByIdWithFootballerandStatistic(int addedFootballerId)
        {
            return _addedFootballerDal.GetWithInclude(
                af => af.Id == addedFootballerId,
                af => af.Footballer,
                af => af.Footballer.Statistic,
                af => af.Footballer.Statistic.PhysicalityDetails,
                af => af.Footballer.Statistic.DefendingDetails,
                af => af.Footballer.Statistic.PaceDetails,
                af => af.Footballer.Statistic.PassingDetails,
                af => af.Footballer.Statistic.DribblingDetails,
                af => af.Footballer.Statistic.ShootingDetails
                );
        }

        public void TUpdate(AddedFootballer entity)
        {
            throw new NotImplementedException();
        }
    }
}
