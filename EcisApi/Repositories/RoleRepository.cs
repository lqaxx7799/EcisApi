using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        Role GetRoleByName(string name);
    }


    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public Role GetRoleByName(string name)
        {
            return db.Set<Role>().Where(x => x.RoleName == name).FirstOrDefault();
        }
    }
}
