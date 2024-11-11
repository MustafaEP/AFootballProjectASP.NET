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
    }
}
