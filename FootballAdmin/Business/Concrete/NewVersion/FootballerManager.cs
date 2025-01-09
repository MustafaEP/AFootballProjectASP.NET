using Access.Abstract.NewVersion;
using Business.Abstract.NewVersion;
using Entities.Concrete.NewVersion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.NewVersion
{
    public class FootballerManager : IFootballerService
    {
        IFootballerDal _footballerDal;

        public FootballerManager(IFootballerDal footballerDal)
        {
            _footballerDal = footballerDal;
        }

        public Footballer GetEveryThink(int id)
        {

            var answer = _footballerDal.GetWithInclude(
                      fo => fo.Id == id,
                      fo => fo.Statistic,
                      fo => fo.Statistic.footballerAbilities,
                      fo => fo.Statistic.FootballerDatas,
                      fo => fo.Statistic.PaceDetails,
                      fo => fo.Statistic.ShootingDetails,
                      fo => fo.Statistic.PassingDetails,
                      fo => fo.Statistic.DribblingDetails,
                      fo => fo.Statistic.DefendingDetails,
                      fo => fo.Statistic.PhysicalityDetails,
                      fo => fo.FootballerPositions);

            return answer;
        }

        public List<Footballer> GetList()
        {
            return _footballerDal.GetListAll();
        }

        public List<Footballer> GetListEveryThink()
        {
            var footballers = _footballerDal.GetListAll();

            var answer = new List<Footballer>();

            foreach (var footballer in footballers)
            {
                answer.Add(_footballerDal.GetWithInclude(
                    fo => fo.Id == footballer.Id,
                    fo => fo.Statistic,
                    fo => fo.Statistic.footballerAbilities,
                    fo => fo.Statistic.FootballerDatas,
                    fo => fo.Statistic.PaceDetails,
                    fo => fo.Statistic.ShootingDetails,
                    fo => fo.Statistic.PassingDetails,
                    fo => fo.Statistic.DribblingDetails,
                    fo => fo.Statistic.DefendingDetails,
                    fo => fo.Statistic.PhysicalityDetails,
                    fo => fo.FootballerPositions
                    ));
            }
            return answer;
        }

        public void TAdd(Footballer entity)
        {
            _footballerDal.Insert(entity);
        }

        public int TCount()
        {
            return _footballerDal.GetListAll().Count();
        }

        public void TDelete(Footballer entity)
        {
            _footballerDal.Delete(entity);
        }

        public Footballer TGetById(int id)
        {
            return _footballerDal.GetById(id);
        }

        public void TUpdate(Footballer entity)
        {
            _footballerDal.Update(entity);
        }
    }
}
