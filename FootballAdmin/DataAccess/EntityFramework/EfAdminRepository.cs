using Access.Abstract;
using Access.EntityFramework.GenericRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.EntityFramework
{
    public class EfAdminRepository : GenericRepository<Admin>, IAdminDal 
    {
    }
}
