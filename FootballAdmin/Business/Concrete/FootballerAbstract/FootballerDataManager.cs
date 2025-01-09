using Access.Abstract.FootballerAbstract;
using Business.Abstract.FootballerAbstract;
using Entities.Concrete.FootballerExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete.FootballerAbstract
{
    public class FootballerDataManager : IFootballerDataService
    {
        IFootballerDataDal _dal;

        public FootballerDataManager(IFootballerDataDal dal)
        {
            _dal = dal;
        }

        public List<FootballerData> GetList()
        {
            return _dal.GetListAll();
        }

        public void TAdd(FootballerData entity)
        {
            _dal.Insert(entity);
        }

        public int TCount()
        {
            throw new NotImplementedException();
        }

        public void TDelete(FootballerData entity)
        {
            _dal.Delete(entity);
        }

        public FootballerData TGetById(int id)
        {
            return _dal.GetById(id);
        }

        public void TUpdate(FootballerData entity)
        {
            _dal.Update(entity);
        }
    }
}
