using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Repositories;
using Microsoft.AspNetCore.Http;
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
        ICollection<AgentAssignment> GetAssignmentsByAgentId(int agentId);
        Agent GetById(int id);
        Agent GetByAccountId(int accountId);
        Task<Agent> AddAsync(AgentCreateDTO payload);
    }

    public class AgentService : IAgentService
    {
        protected readonly IAccountRepository accountRepository;
        protected readonly IAgentRepository agentRepository;
        protected readonly IAgentAssignmentRepository agentAssignmentRepository;
        protected readonly IProvinceRepository provinceRepository;
        protected readonly IRoleRepository roleRepository;

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IEmailHelper emailHelper;

        public AgentService(
            IAccountRepository accountRepository,
            IAgentRepository agentRepository,
            IAgentAssignmentRepository agentAssignmentRepository,
            IProvinceRepository provinceRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IEmailHelper emailHelper
            )
        {
            this.accountRepository = accountRepository;
            this.agentRepository = agentRepository;
            this.agentAssignmentRepository = agentAssignmentRepository;
            this.provinceRepository = provinceRepository;
            this.roleRepository = roleRepository;

            this.unitOfWork = unitOfWork;
            this.emailHelper = emailHelper;
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

        public ICollection<AgentAssignment> GetAssignmentsByAgentId(int agentId)
        {
            return agentAssignmentRepository.GetByAgentId(agentId);
        }

        public async Task<Agent> AddAsync(AgentCreateDTO payload)
        {
            var existingAgent = accountRepository.GetByEmail(payload.Email);
            if (existingAgent != null)
            {
                throw new BadHttpRequestException("EmailExisted");
            }
            foreach (var provinceId in payload.ProvinceIds)
            {
                var province = provinceRepository.GetById(provinceId);
                if (province == null)
                {
                    throw new BadHttpRequestException("InvalidProvince");
                }
            }
            var role = roleRepository.GetRoleByName("Agent");
            if (role == null)
            {
                throw new BadHttpRequestException("RoleNotExisted");
            }

            using var transaction = unitOfWork.BeginTransaction();
            var rawPassword = "abcd1234";

            var account = new Account
            {
                Email = payload.Email,
                IsVerified = true,
                Password = CommonUtils.GenerateSHA1(rawPassword),
                RoleId = role.Id,
                IsDeleted = false,
            };
            await accountRepository.AddAsync(account);
            var agent = new Agent
            {
                IsDeleted = false,
                AccountId = account.Id,
                Address = payload.Address,
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                PhoneNumber = payload.PhoneNumber,
                DateOfBirth = payload.DateOfBirth,
                Gender = payload.Gender
            };
            await agentRepository.AddAsync(agent);
            foreach (var provinceId in payload.ProvinceIds)
            {
                var agentAssignment = new AgentAssignment
                {
                    IsDeleted = false,
                    ProvinceId = provinceId,
                    AgentId = agent.Id
                };
                await agentAssignmentRepository.AddAsync(agentAssignment);
            }
            var mailParams = new Dictionary<string, string>
            {
                { "agentName", $"{agent.LastName} {agent.FirstName}" },
                { "email", account.Email },
                { "password", rawPassword }
            };
            try
            {
                await emailHelper.SendEmailAsync(
                    new string[] { payload.Email },
                    "Thông tin tài khoản đăng nhập",
                    EmailTemplate.AgentCreated,
                    mailParams);
            }
            catch (Exception)
            {

            }
            transaction.Commit();
            return agent;
        }
    }
}
