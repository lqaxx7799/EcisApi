using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IAgentService
    {
        Agent GetById(int id);
        Agent GetByAccountId(int accountId);
    }

    public class AgentService : IAgentService
    {
        protected readonly IAgentRepository agentRepository;
        
        public AgentService(
            IAgentRepository agentRepository
            )
        {
            this.agentRepository = agentRepository;
        }

        public Agent GetById(int id)
        {
            return agentRepository.GetById(id);
        }

        public Agent GetByAccountId(int accountId)
        {
            return agentRepository.GetByAccountId(accountId);
        }
    }
}
