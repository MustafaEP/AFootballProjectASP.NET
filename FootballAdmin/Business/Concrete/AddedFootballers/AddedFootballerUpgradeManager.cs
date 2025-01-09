using Access.Abstract.AddedFootballers;
using Business.Abstract.AddedFootballers;
using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.FootballerAbstract
{
    public class AddedFootballerUpgradeManager : IAddedFootballerUpgradeService
    {
        IAddedFootballerUpgradeDal _addedFootballerUpgradeDal;

        public AddedFootballerUpgradeManager(IAddedFootballerUpgradeDal addedFootballerUpgradeDal)
        {
            _addedFootballerUpgradeDal = addedFootballerUpgradeDal;
        }

        public List<AddedFootballerUpgrade> GetList()
        {
            return _addedFootballerUpgradeDal.GetListAll();
        }

        public void TAdd(AddedFootballerUpgrade entity)
        {
            _addedFootballerUpgradeDal.Insert(entity);
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(AddedFootballerUpgrade entity)
        {
            _addedFootballerUpgradeDal.Delete(entity);
        }

        public AddedFootballerUpgrade TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(AddedFootballerUpgrade entity)
        {
            throw new NotImplementedException();
        }
    }
}
