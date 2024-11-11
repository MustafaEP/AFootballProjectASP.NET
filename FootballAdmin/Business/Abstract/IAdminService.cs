using Business.Abstract.GenericService;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdminService : IGenericService<Admin>
    {
        Admin Login(string username, string password);
    }
}
