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
    public class TrainingManager : ITrainingService
    {
        ITrainingDal _trainingDal;

        public TrainingManager(ITrainingDal trainingDal)
        {
            _trainingDal = trainingDal;
        }

        public List<TrainingSession> GetList()
        {
            return _trainingDal.GetListAll();
        }

        public void TAdd(TrainingSession entity)
        {
            _trainingDal.Insert(entity);
        }

        public int TCount()
        {
            return _trainingDal.GetListAll().Count();
        }

        public void TDelete(TrainingSession entity)
        {
            _trainingDal.Delete(entity);
        }

        public TrainingSession TGetById(int id)
        {
            return _trainingDal.GetById(id);
        }

        public void TUpdate(TrainingSession entity)
        {
            _trainingDal.Update(entity);
        }
    }
}
