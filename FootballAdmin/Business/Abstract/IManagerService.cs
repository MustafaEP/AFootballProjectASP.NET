using Business.Abstract.GenericService;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IManagerService : IGenericService<Manager>
    {
        Manager Login(string username, string password);
        bool PhoneControl(List<Manager> allManager, string phone, int managerId);
        bool EMailControl(List<Manager> allManager, string email, int managerId);
        bool UserNameControl(List<Manager> allManager, string username, int managerId);
        Manager FindManagerWithClubId(int clubId);
    }
}
