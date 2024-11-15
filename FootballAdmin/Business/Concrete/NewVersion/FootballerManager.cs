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
    public class FootballerManager : IFootballerService
    {
        IFootballerDal _footballerDal;

        public FootballerManager(IFootballerDal footballerDal)
        {
            _footballerDal = footballerDal;
        }

        public List<Footballer> GetList()
        {
            return _footballerDal.GetListAll();
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
