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
        ICollection<Agent> GetAllAgents();
        Agent GetByAccountId(int accountId);
    }

    public class AgentRepository : Repository<Agent>, IAgentRepository
    {
        public AgentRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<Agent> GetAllAgents()
        {
            return db.Set<Agent>().Where(x => x.Account.Role.RoleName == "Agent" && !x.IsDeleted).ToList();
        }

        public Agent GetByAccountId(int accountId)
        {
            return db.Set<Agent>().Where(x => x.AccountId == accountId).FirstOrDefault();
        }
    }
}
