using Access.Abstract.FootballerAbstract.Statistics;
using Business.Abstract.FootballerAbstract;
using Entities.Concrete;
using Entities.Concrete.FootballerStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.Abstract.FootballerAbstract
{
    public class FootballerAbilityManager : IFootballerAbilityService
    {
        IFootballerAbilityDal _dal;

        public FootballerAbilityManager(IFootballerAbilityDal dal)
        {
            _dal = dal;
        }

        public List<FootballerAbility> GetAbilities(int statisticId)
        {
            return _dal.GetListAll().Where(x => x.FootballerStatisticId == statisticId).ToList();
        }

        public List<FootballerAbility> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(FootballerAbility entity)
        {
            throw new NotImplementedException();
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerAbility entity)
        {
            throw new NotImplementedException();
        }

        public FootballerAbility TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FootballerAbility entity)
        {
            throw new NotImplementedException();
        }
    }
}
