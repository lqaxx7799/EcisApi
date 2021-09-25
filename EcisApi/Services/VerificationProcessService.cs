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
        ICollection<VerificationProcess> GetByCompany(int companyId);
        VerificationProcess GetById(int id);
        Task<VerificationProcess> AddAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> GenerateAsync(int companyId);
        Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> RequestSupportAsync(int id);
    }

    public class VerificationProcessService : IVerificationProcessService
    {
        protected readonly ICompanyRepository companyRepository;
        protected readonly ICriteriaRepository criteriaRepository;
        protected readonly IVerificationCriteriaRepository verificationCriteriaRepository;
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        public VerificationProcessService(
            ICompanyRepository companyRepository,
            ICriteriaRepository criteriaRepository,
            IVerificationCriteriaRepository verificationCriteriaRepository,
            IVerificationProcessRepository verificationProcessRepository
            )
        {
            this.companyRepository = companyRepository;
            this.criteriaRepository = criteriaRepository;
            this.verificationCriteriaRepository = verificationCriteriaRepository;
            this.verificationProcessRepository = verificationProcessRepository;
        }

        public ICollection<VerificationProcess> GetAll()
        {
            return verificationProcessRepository.GetAll().ToList();
        }

        public ICollection<VerificationProcess> GetAllPending()
        {
            return verificationProcessRepository.Find(x => x.IsSubmitted && !x.IsDeleted);
        }

        public ICollection<VerificationProcess> GetAllSupport()
        {
            return verificationProcessRepository.Find(x =>
                x.SubmitMethod == AppConstants.VerificationProcessSubmitMethod.ByAgent &&
                !x.IsSubmitted &&
                !x.IsDeleted);
        }

        public ICollection<VerificationProcess> GetByCompany(int companyId)
        {
            return verificationProcessRepository.GetByCompany(companyId);
        }

        public VerificationProcess GetById(int id)
        {
            return verificationProcessRepository.GetById(id);
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

            var criterias = criteriaRepository.GetAll();

            foreach (var criteria in criterias.ToList())
            {
                var verificationCriteria = new VerificationCriteria
                {
                    ApprovedStatus = AppConstants.VerificationCriteriaStatus.PEDNING,
                    CriteriaId = criteria.Id,
                    VerificationProcessId = process.Id
                };
                await verificationCriteriaRepository.AddAsync(verificationCriteria);
            }

            return process;
        }

        public async Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess)
        {
            return await verificationProcessRepository.UpdateAsync(verificationProcess);
        }

        public async Task<VerificationProcess> RequestSupportAsync(int id)
        {
            var process = verificationProcessRepository.GetById(id);

            if (process == null)
            {
                throw new BadHttpRequestException("Không tồn tại");
            }

            process.SubmitMethod = AppConstants.VerificationProcessSubmitMethod.ByAgent;
            return await verificationProcessRepository.UpdateAsync(process);
        }
    }
}
