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
        ICollection<VerificationConfirmRequirement> GetPendingByAgentId(int agentId);
        ICollection<VerificationConfirmRequirement> GetFinishedByAgentId(int agentId);
        ICollection<VerificationConfirmRequirement> GetPendingByCompanyId(int companyId);
        VerificationConfirmRequirement GetOneByProcessId(int processId);
        VerificationConfirmRequirement GetById(int id);
        Task<VerificationConfirmRequirement> AddAsync(VerificationConfirmRequirement payload);
        //Task<VerificationConfirmRequirement> AnnounceCompanyAsync(VerificationConfirmUpdateDTO payload);
        Task<VerificationConfirmRequirement> FinishConfirmAsync(VerificationConfirmUpdateDTO payload);
    }

    public class VerificationConfirmRequirementService : IVerificationConfirmRequirementService
    {
        protected readonly IAgentRepository agentRepository;
        protected readonly IVerificationConfirmDocumentRepository verificationConfirmDocumentRepository;
        protected readonly IVerificationConfirmRequirementRepository verificationConfirmRequirementRepository;
        protected readonly IVerificationDocumentRepository verificationDocumentRepository;

        protected readonly IEmailHelper emailHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VerificationConfirmRequirementService(
            IAgentRepository agentRepository,
            IVerificationConfirmDocumentRepository verificationConfirmDocumentRepository,
            IVerificationConfirmRequirementRepository verificationConfirmRequirementRepository,
            IVerificationDocumentRepository verificationDocumentRepository,
            IEmailHelper emailHelper,
            IHttpContextAccessor _httpContextAccessor
            )
        {
            this.agentRepository = agentRepository;
            this.verificationConfirmDocumentRepository = verificationConfirmDocumentRepository;
            this.verificationConfirmRequirementRepository = verificationConfirmRequirementRepository;
            this.verificationDocumentRepository = verificationDocumentRepository;
            this.emailHelper = emailHelper;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public ICollection<VerificationConfirmRequirement> GetPendingByAgentId(int agentId)
        {
            return verificationConfirmRequirementRepository.GetPendingByAgentId(agentId);
        }

        public ICollection<VerificationConfirmRequirement> GetFinishedByAgentId(int agentId)
        {
            var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
            if (role == null)
            {
                return Array.Empty<VerificationConfirmRequirement>();
            }
            if (role.RoleName == "Admin")
            {
                return verificationConfirmRequirementRepository.GetAll().ToList();
            }
            return verificationConfirmRequirementRepository.GetFinishedByAgentId(agentId);
        }

        public ICollection<VerificationConfirmRequirement> GetPendingByCompanyId(int companyId)
        {
            return verificationConfirmRequirementRepository.GetPendingByCompanyId(companyId);
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
            catch (Exception)
            {

            }

            return result;
        }

        //public async Task<VerificationConfirmRequirement> AnnounceCompanyAsync(VerificationConfirmUpdateDTO payload)
        //{
        //    var confirmRequirement = verificationConfirmRequirementRepository.GetById(payload.VerificationConfirmRequirementId);

        //    if (confirmRequirement == null)
        //    {
        //        throw new BadHttpRequestException("VerificationConfirmRequirementNotExist");
        //    }

        //    confirmRequirement.AnnounceCompanyDocumentContent = payload.DocumentContent;
        //    confirmRequirement.AnnounceCompanyDocumentUrl = payload.DocumentUrl;
        //    confirmRequirement.AnnounceCompanyDocumentName = payload.DocumentName;
        //    confirmRequirement.AnnounceCompanyDocumentSize = payload.DocumentSize;
        //    confirmRequirement.AnnounceCompanyDocumentType = payload.DocumentType;
        //    confirmRequirement.IsUsingAnnounceCompanyFile  = payload.IsUsingFile;
        //    confirmRequirement.AnnouncedCompanyAt = DateTime.Now;

        //    await verificationConfirmRequirementRepository.UpdateAsync(confirmRequirement);

        //    try
        //    {
        //        await emailHelper.SendEmailAsync(
        //            new string[] { confirmRequirement.VerificationProcess.Company.Account.Email },
        //            "Yêu cầu xác minh doanh nghiệp",
        //            EmailTemplate.VerificationConfirmRequirementAnnounceCompany,
        //            new Dictionary<string, string>());
        //    }
        //    catch (Exception e)
        //    {

        //    }

        //    return confirmRequirement;
        //}

        public async Task<VerificationConfirmRequirement> FinishConfirmAsync(VerificationConfirmUpdateDTO payload)
        {
            var confirmRequirement = verificationConfirmRequirementRepository.GetById(payload.VerificationConfirmRequirementId);

            if (confirmRequirement == null)
            {
                throw new BadHttpRequestException("VerificationConfirmRequirementNotExist");
            }

            if (confirmRequirement.ConfirmedAt != null)
            {
                throw new BadHttpRequestException("VerificationConfirmRequirementAlreadyFinished");
            }

            confirmRequirement.ConfirmDocumentContent = payload.DocumentContent;
            confirmRequirement.ConfirmedAt = DateTime.Now;

            await verificationConfirmRequirementRepository.UpdateAsync(confirmRequirement);

            foreach (var item in payload.VerificationConfirmDocuments)
            {
                VerificationConfirmDocument document = new()
                {
                    DocumentName = item.DocumentName,
                    DocumentSize = item.DocumentSize,
                    DocumentType = item.DocumentType,
                    DocumentUrl = item.DocumentUrl,
                    VerificationConfirmRequirementId = confirmRequirement.Id
                };
                await verificationConfirmDocumentRepository.AddAsync(document);
            }

            foreach (var item in payload.VerificationConfirmDocuments)
            {
                VerificationDocument document = new()
                {
                    DocumentName = item.DocumentName,
                    ResourceSize = item.DocumentSize,
                    ResourceType = item.DocumentType,
                    ResourceUrl = item.DocumentUrl,
                    VerificationCriteriaId = confirmRequirement.VerificationCriteriaId
                };
                await verificationDocumentRepository.AddAsync(document);
            }

            return confirmRequirement;
        }
    }
}
