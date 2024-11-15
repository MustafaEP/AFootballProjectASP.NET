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
    public class ClubFootballerManager : IClubFootballerService
    {
        IClubFootballerDal _clubfootballerDal;

        public ClubFootballerManager(IClubFootballerDal clubfootballerDal)
        {
            _clubfootballerDal = clubfootballerDal;
        }

        public List<ClubsFootballer> GetList()
        {
            return _clubfootballerDal.GetListAll();
        }

        public List<ClubsFootballer> OwnFootballers(int clubId)
        {
            return _clubfootballerDal.GetListAll().Where(x => x.ManagerClubId == clubId).ToList();
        }

        public void TAdd(ClubsFootballer entity)
        {
            _clubfootballerDal.Insert(entity);
        }

        public int TCount()
        {
            return _clubfootballerDal.GetListAll().Count();
        }

        public void TDelete(ClubsFootballer entity)
        {
            _clubfootballerDal.Delete(entity);
        }

        public ClubsFootballer TGetById(int id)
        {
            return _clubfootballerDal.GetById(id);
        }

        public void TUpdate(ClubsFootballer entity)
        {
            _clubfootballerDal.Update(entity);
        }
    }
}
