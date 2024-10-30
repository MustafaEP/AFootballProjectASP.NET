using Business.Abstract;
using Access.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlayerManager : IPlayerService
    {
        IPlayerDal _playerDal;

        public PlayerManager(IPlayerDal playerDal)
        {
            _playerDal = playerDal;
        }

        public List<Player> GetList()
        {
            return _playerDal.GetListAll();
        }

        public Player TGetById(int id)
        {
            return _playerDal.GetById(id);
        }

        public void TAdd(Player entity)
        {
            _playerDal.Insert(entity);
        }

        public void TDelete(Player entity)
        {
            _playerDal.Delete(entity);
        }


        public void TUpdate(Player entity)
        {
            _playerDal.Update(entity);
        }
    }
}
