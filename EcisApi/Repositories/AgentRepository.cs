using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IAgentRepository : IRepository<Agent>
    {
        Agent GetByAccountId(int accountId);
    }

    public class AgentRepository : Repository<Agent>, IAgentRepository
    {
        public AgentRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public Agent GetByAccountId(int accountId)
        {
            return db.Set<Agent>().Where(x => x.AccountId == accountId).FirstOrDefault();
        }
    }
}
