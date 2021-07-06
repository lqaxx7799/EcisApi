using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IAccountRepository
    {

    }


    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public Account FindById(int id)
        {
            try
            {
                return db.Set<Account>().SingleOrDefault(x => x.ID == id);
            }
            catch (Exception)
            {
                throw new Exception("Cannot find account by id");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var account = FindById(id);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            try
            {
                account.IsDeleted = true;
                await UpdateAsync(account);
            }
            catch (Exception)
            {
                throw new Exception("Cannot delete account");
            }
        }
    }
}
