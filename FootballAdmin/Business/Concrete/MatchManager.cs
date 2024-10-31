using Access.Abstract;
using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MatchManager : IMatchSservice
    {
        IMatchDal _matchDal;

        public MatchManager(IMatchDal matchDal)
        {
            _matchDal = matchDal;
        }

        public List<Match> GetList()
        {
            return _matchDal.GetListAll();
        }
        public Match GetLast()
        {
            var list = _matchDal.GetListAll();
            if(list != null)
            {
                return list.Last();
            }
            return null;
        }

        public Match TGetById(int id)
        {
            return _matchDal.GetById(id);
        }

        public void TAdd(Match entity)
        {
            _matchDal.Insert(entity);
        }

        public void TDelete(Match entity)
        {
            _matchDal.Delete(entity);
        }


        public void TUpdate(Match entity)
        {
            _matchDal.Update(entity);
        }

        public int TCount()
        {
            return _matchDal.GetListAll().Count();
        }
    }
}
