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
        ICollection<Agent> GetAll();
        ICollection<Agent> GetAllAgents();
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

        public ICollection<Agent> GetAll()
        {
            return agentRepository.GetAll().ToList();
        }

        public ICollection<Agent> GetAllAgents()
        {
            return agentRepository.GetAllAgents();
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
