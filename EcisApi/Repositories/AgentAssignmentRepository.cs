using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IAgentAssignmentRepository : IRepository<AgentAssignment>
    {

    }

    public class AgentAssignmentRepository : Repository<AgentAssignment>, IAgentAssignmentRepository
    {
        public AgentAssignmentRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
