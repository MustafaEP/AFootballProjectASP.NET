using Business.Abstract.GenericService;
using Entities.Concrete.ClubFormation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.ClubFormation
{
    public interface IFormationService : IGenericService<Formation>
    {
        Formation GetByClubId(int managerClubId);
    }
}
