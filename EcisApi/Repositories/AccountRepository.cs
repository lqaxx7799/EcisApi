using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IAccountRepository: IRepository<Account>
    {
        Account GetByEmail(string email);
    }


    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public Account GetByEmail(string email)
        {
            return db.Set<Account>().Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
