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
    public interface IVerificationProcessService
    {
        ICollection<VerificationProcess> GetAll();
        ICollection<VerificationProcess> GetAllPending();
        ICollection<VerificationProcess> GetAllSupport();
        ICollection<VerificationProcess> GetAllReviewed();
        ICollection<VerificationProcess> GetByCompany(int companyId);
        VerificationProcess GetById(int id);
        VerificationProcess GetCompanyCurrentPending(int companyId);
        VerificationProcess GetCompanyLast(int companyId);
        Task<VerificationProcess> AddAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> GenerateAsync(int companyId);
        Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> SubmitProcessAsync(int id);
        Task<VerificationProcess> SubmitReviewAsync(int id);
        Task<VerificationProcess> RequestSupportAsync(int id);
        Task<VerificationProcess> FinishAsync(int id);
    }

    public class VerificationProcessService : IVerificationProcessService
    {
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        protected readonly ICriteriaDetailRepository criteriaDetailRepository;
        protected readonly IVerificationCriteriaRepository verificationCriteriaRepository;
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        protected readonly IEmailHelper emailHelper;

        public VerificationProcessService(
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            ICriteriaDetailRepository criteriaDetailRepository,
            IVerificationCriteriaRepository verificationCriteriaRepository,
            IVerificationProcessRepository verificationProcessRepository,
            IEmailHelper emailHelper
            )
        {
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.criteriaDetailRepository = criteriaDetailRepository;
            this.verificationCriteriaRepository = verificationCriteriaRepository;
            this.verificationProcessRepository = verificationProcessRepository;

            this.emailHelper = emailHelper;
        }

        public ICollection<VerificationProcess> GetAll()
        {
            return verificationProcessRepository.GetAll().ToList();
        }

        public ICollection<VerificationProcess> GetAllPending()
        {
            return verificationProcessRepository.Find(x => x.IsSubmitted && !x.IsReviewed && !x.IsDeleted);
        }

        public ICollection<VerificationProcess> GetAllSupport()
        {
            return verificationProcessRepository.Find(x => !x.IsSubmitted && !x.IsDeleted);
        }

        public ICollection<VerificationProcess> GetAllReviewed()
        {
            return verificationProcessRepository.Find(x => x.IsReviewed && !x.IsDeleted);
        }

        public ICollection<VerificationProcess> GetByCompany(int companyId)
        {
            return verificationProcessRepository.GetByCompany(companyId);
        }

        public VerificationProcess GetById(int id)
        {
            return verificationProcessRepository.GetById(id);
        }

        public VerificationProcess GetCompanyCurrentPending(int companyId)
        {
            return verificationProcessRepository
                .Find(x => x.CompanyId == companyId && !x.IsDeleted && !x.IsFinished)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();
        }

        public VerificationProcess GetCompanyLast(int companyId)
        {
            return verificationProcessRepository
                .Find(x => x.CompanyId == companyId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();
        }

        public async Task<VerificationProcess> AddAsync(VerificationProcess verificationProcess)
        {
            return await verificationProcessRepository.AddAsync(verificationProcess);
        }

        public async Task<VerificationProcess> GenerateAsync(int companyId)
        {
            var company = companyRepository.GetById(companyId);
            if (company == null || company.IsDeleted)
            {
                throw new BadHttpRequestException("CompanyNotExist");
            }

            var process = new VerificationProcess
            {
                IsDeleted = false,
                CompanyId = company.Id,
                IsSubmitted = false,
                IsOpenedByAgent = false,
                SubmitMethod = AppConstants.VerificationProcessSubmitMethod.ByCustomer,
                SubmitDeadline = DateTime.Now.AddDays(10),
            };
            await verificationProcessRepository.AddAsync(process);

            var criteriaDetails = criteriaDetailRepository.GetAll();

            foreach (var criteriaDetail in criteriaDetails.ToList())
            {
                var verificationCriteria = new VerificationCriteria
                {
                    ApprovedStatus = AppConstants.VerificationCriteriaStatus.PEDNING,
                    CriteriaDetailId = criteriaDetail.Id,
                    VerificationProcessId = process.Id
                };
                await verificationCriteriaRepository.AddAsync(verificationCriteria);
            }

            return process;
        }

        public async Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess)
        {
            var process = verificationProcessRepository.GetById(verificationProcess.Id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }

            process.AssignedAgentId = verificationProcess.AssignedAgentId;
            process.CompanyTypeId = verificationProcess.CompanyTypeId;
            process.IsFinished = verificationProcess.IsFinished;
            process.IsOpenedByAgent = verificationProcess.IsOpenedByAgent;
            process.IsReviewed = verificationProcess.IsReviewed;
            process.IsSubmitted = verificationProcess.IsSubmitted;
            process.ReviewedAt = verificationProcess.ReviewedAt;
            process.Status = verificationProcess.Status;
            process.SubmitDeadline = verificationProcess.SubmitDeadline;
            process.SubmittedAt = verificationProcess.SubmittedAt;
            process.ValidFrom = verificationProcess.ValidFrom;
            process.ValidTo = verificationProcess.ValidTo;

            return await verificationProcessRepository.UpdateAsync(process);
        }

        public async Task<VerificationProcess> SubmitProcessAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }

            process.IsSubmitted = true;
            process.SubmittedAt = DateTime.Now;
            return await verificationProcessRepository.UpdateAsync(process);
        }
        
        public async Task<VerificationProcess> SubmitReviewAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }

            process.IsReviewed = true;
            process.ReviewedAt = DateTime.Now;
            return await verificationProcessRepository.UpdateAsync(process);
        }

        public async Task<VerificationProcess> RequestSupportAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }

            process.SubmitMethod = AppConstants.VerificationProcessSubmitMethod.ByAgent;
            return await verificationProcessRepository.UpdateAsync(process);
        }

        public async Task<VerificationProcess> FinishAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }

            process.IsFinished = true;
            process.FinishedAt = DateTime.Now;
            await verificationProcessRepository.UpdateAsync(process);

            Company company = companyRepository.GetById(process.CompanyId);

            CompanyTypeModification currentModification = new()
            {
                CompanyId = process.CompanyId,
                PreviousCompanyTypeId = company.CompanyTypeId,
                UpdatedCompanyTypeId = process.CompanyTypeId,
                Modification = AppConstants.CompanyModificationType.VERIFICATION,
                VerificationProcessId = process.Id,
            };
            await companyTypeModificationRepository.AddAsync(currentModification);

            company.CompanyTypeId = process.CompanyTypeId;
            await companyRepository.UpdateAsync(company);

            try
            {
                await emailHelper.SendEmailAsync(
                   new string[] { process.Company.Account.Email },
                   "Kết quả đánh giá doanh nghiệp",
                   EmailTemplate.VerificationFinished,
                   new Dictionary<string, string>());
            }
            catch (Exception e)
            {

            }

            return process;
        }
    }
}
