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
    public interface IVerificationProcessService
    {
        ICollection<VerificationProcess> GetAll();
        ICollection<VerificationProcess> GetAllPending();
        ICollection<VerificationProcess> GetAllSupport();
        ICollection<VerificationProcess> GetAllReviewed();
        //ICollection<VerificationProcess> GetAllClassified();
        ICollection<VerificationProcess> GetByCompany(int companyId);
        ICollection<VerificationProcessRatingDTO> GetRatingCount(int[] processIds);
        VerificationProcess GetById(int id);
        VerificationProcess GetCompanyCurrentPending(int companyId);
        VerificationProcess GetCompanyLast(int companyId);
        Task<VerificationProcess> AddAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> GenerateAsync(int companyId);
        Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> SubmitProcessAsync(int id);
        Task<VerificationProcess> SubmitReviewAsync(int id, int assignedAgentId);
        //Task<VerificationProcess> SubmitClassifyAsync(int id, int companyTypeId);
        Task<VerificationProcess> RequestSupportAsync(int id);
        Task<VerificationProcess> RejectReviewedAsync(int id);
        Task<VerificationProcess> FinishAsync(int id, int companyTypeId);
        //Task<VerificationProcess> RejectClassifiedAsync(int id);
    }

    public class VerificationProcessService : IVerificationProcessService
    {
        protected readonly IAgentRepository agentRepository;
        protected readonly IAgentAssignmentRepository agentAssignmentRepository;
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICompanyTypeModificationRepository companyTypeModificationRepository;
        protected readonly ICriteriaDetailRepository criteriaDetailRepository;
        protected readonly IVerificationCriteriaRepository verificationCriteriaRepository;
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IEmailHelper emailHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VerificationProcessService(
            IAgentRepository agentRepository,
            IAgentAssignmentRepository agentAssignmentRepository,
            ICompanyRepository companyRepository,
            ICompanyTypeModificationRepository companyTypeModificationRepository,
            ICriteriaDetailRepository criteriaDetailRepository,
            IVerificationCriteriaRepository verificationCriteriaRepository,
            IVerificationProcessRepository verificationProcessRepository,
            IUnitOfWork unitOfWork,
            IEmailHelper emailHelper,
            IHttpContextAccessor _httpContextAccessor
            )
        {
            this.agentRepository = agentRepository;
            this.agentAssignmentRepository = agentAssignmentRepository;
            this.companyRepository = companyRepository;
            this.companyTypeModificationRepository = companyTypeModificationRepository;
            this.criteriaDetailRepository = criteriaDetailRepository;
            this.verificationCriteriaRepository = verificationCriteriaRepository;
            this.verificationProcessRepository = verificationProcessRepository;

            this.unitOfWork = unitOfWork;
            this.emailHelper = emailHelper;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public ICollection<VerificationProcess> GetAll()
        {
            var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
            if (role == null)
            {
                return Array.Empty<VerificationProcess>();
            }
            if (role.RoleName == "Admin")
            {
                return verificationProcessRepository.GetAll().ToList();
            }
            var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
            var agent = agentRepository.GetByAccountId(account.Id);
            var assigneds = agentAssignmentRepository.GetByAgentId(agent.Id);
            var provinceIds = assigneds.Select(x => x.ProvinceId).ToList();
            return verificationProcessRepository.Find(x => provinceIds.Contains(x.Company.ProvinceId.Value));
        }

        public ICollection<VerificationProcess> GetAllPending()
        {
            var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
            if (role == null)
            {
                return Array.Empty<VerificationProcess>();
            }
            var processes = verificationProcessRepository
                    .Find(x => x.Status == AppConstants.VerificationProcessStatus.Submitted && !x.IsDeleted);

            if (role.RoleName == "Admin")
            {
                return processes.ToList();
            }
            var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
            var agent = agentRepository.GetByAccountId(account.Id);
            var assigneds = agentAssignmentRepository.GetByAgentId(agent.Id);
            var provinceIds = assigneds.Select(x => x.ProvinceId).ToList();
            return processes
                .Where(x => provinceIds.Contains(x.Company.ProvinceId.Value))
                .ToList();
        }

        public ICollection<VerificationProcess> GetAllSupport()
        {
            var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
            if (role == null)
            {
                return Array.Empty<VerificationProcess>();
            }
            var processes = verificationProcessRepository
                    .Find(x => (x.Status == AppConstants.VerificationProcessStatus.InProgress || x.Status == AppConstants.VerificationProcessStatus.Submitted) && !x.IsDeleted);
            
            if (role.RoleName == "Admin")
            {
                return processes.ToList();
            }
            var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
            var agent = agentRepository.GetByAccountId(account.Id);
            var assigneds = agentAssignmentRepository.GetByAgentId(agent.Id);
            var provinceIds = assigneds.Select(x => x.ProvinceId).ToList();
            return processes
                .Where(x => provinceIds.Contains(x.Company.ProvinceId.Value))
                .ToList();
        }

        public ICollection<VerificationProcess> GetAllReviewed()
        {
            var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
            if (role == null)
            {
                return Array.Empty<VerificationProcess>();
            }
            var processes = verificationProcessRepository
                    .Find(x => x.Status == AppConstants.VerificationProcessStatus.Reviewed && !x.IsDeleted);
            if (role.RoleName == "Admin")
            {
                return processes.ToList();
            }
            var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
            var agent = agentRepository.GetByAccountId(account.Id);
            var assigneds = agentAssignmentRepository.GetByAgentId(agent.Id);
            var provinceIds = assigneds.Select(x => x.ProvinceId).ToList();
            return processes
                .Where(x => provinceIds.Contains(x.Company.ProvinceId.Value))
                .ToList();
        }

        //public ICollection<VerificationProcess> GetAllClassified()
        //{
        //    var role = (Role)_httpContextAccessor.HttpContext.Items["Role"];
        //    if (role == null)
        //    {
        //        return Array.Empty<VerificationProcess>();
        //    }
        //    var processes = verificationProcessRepository
        //            .Find(x => x.Status == AppConstants.VerificationProcessStatus.Classified && !x.IsDeleted);
        //    if (role.RoleName == "Admin")
        //    {
        //        return processes.ToList();
        //    }
        //    var account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
        //    var agent = agentRepository.GetByAccountId(account.Id);

        //    return processes
        //        .Where(x => x.AssignedAgentId == agent.Id)
        //        .ToList();
        //}

        public ICollection<VerificationProcess> GetByCompany(int companyId)
        {
            return verificationProcessRepository.GetByCompany(companyId);
        }

        public ICollection<VerificationProcessRatingDTO> GetRatingCount(int[] processIds)
        {
            var results = new List<VerificationProcessRatingDTO>();
            foreach (var processId in processIds)
            {
                var criterias = verificationCriteriaRepository.GetByProcessId(processId);
                VerificationProcessRatingDTO rating = new()
                {
                    VerificationProcessId = processId,
                    TotalCount = criterias.Count,
                    PendingCount = criterias.Where(x => x.ApprovedStatus == AppConstants.VerificationCriteriaStatus.PENDING).Count(),
                    RejectedCount = criterias.Where(x => x.ApprovedStatus == AppConstants.VerificationCriteriaStatus.REJECTED).Count(),
                    VerifiedCount = criterias.Where(x => x.ApprovedStatus == AppConstants.VerificationCriteriaStatus.VERIFIED).Count(),
                };
                results.Add(rating);
            }
            return results;
        }

        public VerificationProcess GetById(int id)
        {
            return verificationProcessRepository.GetById(id);
        }

        public VerificationProcess GetCompanyCurrentPending(int companyId)
        {
            return verificationProcessRepository
                .Find(x => 
                    x.CompanyId == companyId &&
                    !x.IsDeleted && (
                        x.Status == AppConstants.VerificationProcessStatus.InProgress ||
                        x.Status == AppConstants.VerificationProcessStatus.Submitted
                    )
                )
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

            var pendingProcess = verificationProcessRepository
                .Find(x =>
                    x.CompanyId == companyId
                    && x.IsDeleted
                    && x.Status != AppConstants.VerificationProcessStatus.Finished)
                .FirstOrDefault();
            if (pendingProcess != null)
            {
                throw new BadHttpRequestException("HasUnfinishedVerificationProcess");
            }

            using var transaction = unitOfWork.BeginTransaction();
            var process = new VerificationProcess
            {
                IsDeleted = false,
                CompanyId = company.Id,
                IsSubmitted = false,
                IsOpenedByAgent = false,
                SubmitMethod = AppConstants.VerificationProcessSubmitMethod.ByCustomer,
                SubmitDeadline = DateTime.Now.AddDays(10),
                Status = AppConstants.VerificationProcessStatus.InProgress,
            };
            await verificationProcessRepository.AddAsync(process);

            var criteriaDetails = criteriaDetailRepository.GetAll();

            foreach (var criteriaDetail in criteriaDetails.ToList())
            {
                var verificationCriteria = new VerificationCriteria
                {
                    ApprovedStatus = AppConstants.VerificationCriteriaStatus.PENDING,
                    CriteriaDetailId = criteriaDetail.Id,
                    VerificationProcessId = process.Id
                };
                await verificationCriteriaRepository.AddAsync(verificationCriteria);
            }
            transaction.Commit();
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
            if (process.Status != AppConstants.VerificationProcessStatus.InProgress)
            {
                throw new BadHttpRequestException("InvalidVerificationProcess");
            }
            process.IsSubmitted = true;
            process.SubmittedAt = DateTime.Now;
            process.Status = AppConstants.VerificationProcessStatus.Submitted;
            return await verificationProcessRepository.UpdateAsync(process);
        }
        
        public async Task<VerificationProcess> SubmitReviewAsync(int id, int assignedAgentId)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }
            if (process.Status != AppConstants.VerificationProcessStatus.Submitted)
            {
                throw new BadHttpRequestException("InvalidVerificationProcess");
            }

            process.IsReviewed = true;
            process.ReviewedAt = DateTime.Now;
            process.Status = AppConstants.VerificationProcessStatus.Reviewed;
            process.AssignedAgentId = assignedAgentId;
            return await verificationProcessRepository.UpdateAsync(process);
        }

        public async Task<VerificationProcess> RequestSupportAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }
            if (
                process.Status != AppConstants.VerificationProcessStatus.InProgress &&
                process.Status != AppConstants.VerificationProcessStatus.Submitted
                )
            {
                throw new BadHttpRequestException("InvalidVerificationProcess");
            }

            process.SubmitMethod = AppConstants.VerificationProcessSubmitMethod.ByAgent;
            return await verificationProcessRepository.UpdateAsync(process);
        }

        //public async Task<VerificationProcess> SubmitClassifyAsync(int id, int companyTypeId)
        //{
        //    var process = verificationProcessRepository.GetById(id);

        //    if (process == null)
        //    {
        //        throw new BadHttpRequestException("VerificationProcessNotExist");
        //    }
        //    if (process.Status != AppConstants.VerificationProcessStatus.Reviewed)
        //    {
        //        throw new BadHttpRequestException("InvalidVerificationProcess");
        //    }

        //    process.IsReviewed = true;
        //    process.ReviewedAt = DateTime.Now;
        //    process.Status = AppConstants.VerificationProcessStatus.Classified;
        //    process.CompanyTypeId = companyTypeId;
        //    return await verificationProcessRepository.UpdateAsync(process);
        //}

        public async Task<VerificationProcess> RejectReviewedAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }
            if (process.Status != AppConstants.VerificationProcessStatus.Reviewed)
            {
                throw new BadHttpRequestException("InvalidVerificationProcess");
            }

            process.Status = AppConstants.VerificationProcessStatus.Submitted;
            return await verificationProcessRepository.UpdateAsync(process);
        }

        public async Task<VerificationProcess> FinishAsync(int id, int companyTypeId)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("VerificationProcessNotExist");
            }
            if (process.Status != AppConstants.VerificationProcessStatus.Reviewed)
            {
                throw new BadHttpRequestException("InvalidVerificationProcess");
            }

            using var transaction = unitOfWork.BeginTransaction();
            process.IsFinished = true;
            process.FinishedAt = DateTime.Now;
            process.CompanyTypeId = companyTypeId;
            process.Status = AppConstants.VerificationProcessStatus.Finished;
            await verificationProcessRepository.UpdateAsync(process);

            Company company = companyRepository.GetById(process.CompanyId);

            CompanyTypeModification currentModification = new()
            {
                CompanyId = process.CompanyId,
                PreviousCompanyTypeId = company.CompanyTypeId,
                UpdatedCompanyTypeId = companyTypeId,
                Modification = AppConstants.CompanyModificationType.VERIFICATION,
                VerificationProcessId = process.Id,
            };
            await companyTypeModificationRepository.AddAsync(currentModification);

            company.CompanyTypeId = companyTypeId;
            await companyRepository.UpdateAsync(company);

            try
            {
                await emailHelper.SendEmailAsync(
                    new string[] { process.Company.Account.Email },
                    "Kết quả đánh giá doanh nghiệp",
                    EmailTemplate.VerificationFinished,
                    new Dictionary<string, string>());
            }
            catch (Exception)
            {
                Console.WriteLine("FinishAsync SendEmail Error");
            }

            transaction.Commit();
            return process;
        }

        //public async Task<VerificationProcess> RejectClassifiedAsync(int id)
        //{
        //    var process = verificationProcessRepository.GetById(id);

        //    if (process == null)
        //    {
        //        throw new BadHttpRequestException("VerificationProcessNotExist");
        //    }
        //    if (process.Status != AppConstants.VerificationProcessStatus.Classified)
        //    {
        //        throw new BadHttpRequestException("InvalidVerificationProcess");
        //    }

        //    process.Status = AppConstants.VerificationProcessStatus.Reviewed;
        //    return await verificationProcessRepository.UpdateAsync(process);
        //}
    }
}
