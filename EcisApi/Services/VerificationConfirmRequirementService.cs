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
    public interface IVerificationConfirmRequirementService
    {
        ICollection<VerificationConfirmRequirement> GetByAgentId(int agentId);
        VerificationConfirmRequirement GetOneByProcessId(int processId);
        VerificationConfirmRequirement GetById(int id);
        Task<VerificationConfirmRequirement> AddAsync(VerificationConfirmRequirement payload);
        Task<VerificationConfirmRequirement> AnnounceCompanyAsync(VerificationConfirmUpdateDTO payload);
        Task<VerificationConfirmRequirement> FinishConfirmAsync(VerificationConfirmUpdateDTO payload);
    }

    public class VerificationConfirmRequirementService : IVerificationConfirmRequirementService
    {
        protected readonly IAgentRepository agentRepository;
        protected readonly IVerificationConfirmRequirementRepository verificationConfirmRequirementRepository;

        protected readonly IEmailHelper emailHelper;

        public VerificationConfirmRequirementService(
            IAgentRepository agentRepository,
            IVerificationConfirmRequirementRepository verificationConfirmRequirementRepository,
            IEmailHelper emailHelper
            )
        {
            this.agentRepository = agentRepository;
            this.verificationConfirmRequirementRepository = verificationConfirmRequirementRepository;
            this.emailHelper = emailHelper;
        }

        public ICollection<VerificationConfirmRequirement> GetByAgentId(int agentId)
        {
            return verificationConfirmRequirementRepository.GetByAgentId(agentId);
        }

        public VerificationConfirmRequirement GetOneByProcessId(int processId)
        {
            return verificationConfirmRequirementRepository.GetOneByProcessId(processId);
        }

        public VerificationConfirmRequirement GetById(int id)
        {
            return verificationConfirmRequirementRepository.GetById(id);
        }

        public async Task<VerificationConfirmRequirement> AddAsync(VerificationConfirmRequirement payload)
        {
            if (payload.AssignedAgentId == null)
            {
                throw new BadHttpRequestException("EmptyAssignedAgent");
            }
            payload.AnnouncedAgentAt = DateTime.Now;
            var result = await verificationConfirmRequirementRepository.AddAsync(payload);

            var agent = agentRepository.GetById(payload.AssignedAgentId);

            try
            {
                await emailHelper.SendEmailAsync(
                    new string[] { agent.Account.Email },
                    "Yêu cầu xác minh doanh nghiệp",
                    EmailTemplate.VerificationConfirmRequirementAnnounceAgent,
                    new Dictionary<string, string>());
            }
            catch (Exception e)
            {

            }

            return result;
        }

        public async Task<VerificationConfirmRequirement> AnnounceCompanyAsync(VerificationConfirmUpdateDTO payload)
        {
            var confirmRequirement = verificationConfirmRequirementRepository.GetById(payload.VerificationConfirmRequirementId);

            if (confirmRequirement == null)
            {
                throw new BadHttpRequestException("VerificationConfirmRequirementNotExist");
            }

            confirmRequirement.AnnounceCompanyDocumentContent = payload.DocumentContent;
            confirmRequirement.AnnounceCompanyDocumentUrl = payload.DocumentUrl;
            confirmRequirement.AnnounceCompanyDocumentName = payload.DocumentName;
            confirmRequirement.AnnounceCompanyDocumentSize = payload.DocumentSize;
            confirmRequirement.AnnounceCompanyDocumentType = payload.DocumentType;
            confirmRequirement.IsUsingAnnounceCompanyFile  = payload.IsUsingFile;
            confirmRequirement.AnnouncedCompanyAt = DateTime.Now;

            await verificationConfirmRequirementRepository.UpdateAsync(confirmRequirement);

            try
            {
                await emailHelper.SendEmailAsync(
                    new string[] { confirmRequirement.VerificationProcess.Company.Account.Email },
                    "Yêu cầu xác minh doanh nghiệp",
                    EmailTemplate.VerificationConfirmRequirementAnnounceCompany,
                    new Dictionary<string, string>());
            }
            catch (Exception e)
            {

            }

            return confirmRequirement;
        }

        public async Task<VerificationConfirmRequirement> FinishConfirmAsync(VerificationConfirmUpdateDTO payload)
        {
            var confirmRequirement = verificationConfirmRequirementRepository.GetById(payload.VerificationConfirmRequirementId);

            if (confirmRequirement == null)
            {
                throw new BadHttpRequestException("VerificationConfirmRequirementNotExist");
            }

            confirmRequirement.ConfirmDocumentContent = payload.DocumentContent;
            confirmRequirement.ConfirmDocumentUrl = payload.DocumentUrl;
            confirmRequirement.ConfirmDocumentName = payload.DocumentName;
            confirmRequirement.ConfirmDocumentSize = payload.DocumentSize;
            confirmRequirement.ConfirmDocumentType = payload.DocumentType;
            confirmRequirement.IsUsingConfirmFile = payload.IsUsingFile;
            confirmRequirement.ConfirmedAt = DateTime.Now;
            confirmRequirement.ConfirmCompanyTypeId = payload.CompanyTypeId;

            await verificationConfirmRequirementRepository.UpdateAsync(confirmRequirement);

            return confirmRequirement;
        }
    }
}
