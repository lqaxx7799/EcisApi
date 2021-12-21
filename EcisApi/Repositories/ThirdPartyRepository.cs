using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IThirdPartyRepository : IRepository<ThirdParty>
    {
        new ICollection<ThirdParty> GetAll();
        ThirdParty GetByAccountId(int accountId);
    }

    public class ThirdPartyRepository : Repository<ThirdParty>, IThirdPartyRepository
    {
        public ThirdPartyRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public new ICollection<ThirdParty> GetAll()
        {
            return db.Set<ThirdParty>().Where(x => !x.IsDeleted).ToList();
        }

        public ThirdParty GetByAccountId(int accountId)
        {
            return db.Set<ThirdParty>().Where(x => !x.IsDeleted && x.AccountId == accountId).FirstOrDefault();
        }
    }
}
