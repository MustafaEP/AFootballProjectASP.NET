using Business.Abstract.GenericService;
using Entities.Concrete;
using Entities.Concrete.AddedFootballersExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.AddedFootballers
{
    public interface IAddedFootballerService : IGenericService<AddedFootballer>
    {
        bool isAlone(int clubId, int footballerId);
        AddedFootballer GetAddedFootballer(int clubId, int footballerId);
        List<AddedFootballer> GetAddedsClub(int clubId);
        AddedFootballer GetByIdWithFootballer(int addedFootballerId);
    }
}
