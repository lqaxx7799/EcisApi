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
        VerificationConfirmRequirement GetOneByProcessId(int processId);
        Task<VerificationConfirmRequirement> AddAsync(VerificationConfirmRequirement payload);
        Task<VerificationConfirmRequirement> AnnounceCompanyAsync(VerificationConfirmUpdateDTO payload);
        Task<VerificationConfirmRequirement> FinishConfirmAsync(VerificationConfirmUpdateDTO payload);
    }

    public class VerificationConfirmRequirementService : IVerificationConfirmRequirementService
    {
        protected readonly IVerificationConfirmRequirementRepository verificationConfirmRequirementRepository;

        protected readonly IEmailHelper emailHelper;

        public VerificationConfirmRequirementService(
            IVerificationConfirmRequirementRepository verificationConfirmRequirementRepository,
            IEmailHelper emailHelper
            )
        {
            this.verificationConfirmRequirementRepository = verificationConfirmRequirementRepository;
            this.emailHelper = emailHelper;
        }

        public VerificationConfirmRequirement GetOneByProcessId(int processId)
        {
            return verificationConfirmRequirementRepository.GetOneByProcessId(processId);
        }

        public async Task<VerificationConfirmRequirement> AddAsync(VerificationConfirmRequirement payload)
        {
            if (payload.AssignedAgentId == null)
            {
                throw new BadHttpRequestException("EmptyAssignedAgent");
            }
            payload.AnnouncedAgentAt = DateTime.Now;
            var result = await verificationConfirmRequirementRepository.AddAsync(payload);

            await emailHelper.SendEmailAsync(
                new string[] { result.AssignedAgent.Account.Email },
                "Yêu cầu xác minh doanh nghiệp",
                EmailTemplate.VerificationConfirmRequirementAnnounceAgent,
                new Dictionary<string, string>());

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

            await emailHelper.SendEmailAsync(
                new string[] { confirmRequirement.VerificationProcess.Company.Account.Email },
                "Yêu cầu xác minh doanh nghiệp",
                EmailTemplate.VerificationConfirmRequirementAnnounceCompany,
                new Dictionary<string, string>());

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

            //await emailHelper.SendEmailAsync(
            //    new string[] { confirmRequirement.VerificationProcess.Company.Account.Email },
            //    "Yêu cầu xác minh doanh nghiệp",
            //    EmailTemplate.VerificationConfirmRequirementAnnounceCompany,
            //    new Dictionary<string, string>());

            return confirmRequirement;
        }
    }
}
