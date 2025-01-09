using Access.Abstract.ClubFormation;
using Business.Abstract.ClubFormation;
using Entities.Concrete.ClubFormation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.ClubFormation
{
    public class XIPlayerManagere : IXIPlayerService
    {
        IXIPlayerDal _dal;

        public XIPlayerManagere(IXIPlayerDal dal)
        {
            _dal = dal;
        }

        public List<XIPlayer> GetFormationPlayer(int formationId)
        {
            return _dal.GetListAll().Where(x => x.FormationId == formationId).ToList();
        }

        public List<XIPlayer> GetList()
        {
            throw new NotImplementedException();
        }

        public XIPlayer GetXIPlayerinFormation(int formationId, int? addedFootballerId)
        {
            if (addedFootballerId == null) return null;
            return _dal.GetListAll().FirstOrDefault(x => x.FormationId == formationId && x.AddedFootballerId == addedFootballerId);
        }


        public void TAdd(XIPlayer entity)
        {
            _dal.Insert(entity);
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(XIPlayer entity)
        {
            _dal.Delete(entity);
        }

        public XIPlayer TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(XIPlayer entity)
        {
            _dal.Update(entity);
        }

        public void UpdateXI(List<XIPlayer> xIPlayers)
        {
            foreach(var entity in xIPlayers)
            {
                _dal.Update(entity);
            }
        }
    }
}
