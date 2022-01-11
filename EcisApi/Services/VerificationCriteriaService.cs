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
    public interface IVerificationCriteriaService
    {
        ICollection<VerificationCriteria> GetByProcessId(int processId);
        Task<VerificationCriteria> AddAsync(VerificationCriteria payload);
        Task<VerificationCriteria> UpdateAsync(VerificationCriteria payload);
        Task<ICollection<VerificationCriteria>> ApproveAllAsync(int processId);
    }

    public class VerificationCriteriaService : IVerificationCriteriaService
    {
        protected readonly IVerificationCriteriaRepository verificationCriteriaRepository;
        protected readonly IUnitOfWork unitOfWork;

        public VerificationCriteriaService(
            IVerificationCriteriaRepository verificationCriteriaRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.verificationCriteriaRepository = verificationCriteriaRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICollection<VerificationCriteria> GetByProcessId(int processId)
        {
            return verificationCriteriaRepository.GetByProcessId(processId);
        }

        public async Task<VerificationCriteria> AddAsync(VerificationCriteria payload)
        {
            return await verificationCriteriaRepository.AddAsync(payload);
        }

        public async Task<VerificationCriteria> UpdateAsync(VerificationCriteria payload)
        {
            var verificationCriteria = verificationCriteriaRepository.GetById(payload.Id);
            if (verificationCriteria == null)
            {
                throw new BadHttpRequestException("VerificationCriteriaNotExist");
            }
            verificationCriteria.ApprovedStatus = payload.ApprovedStatus;
            verificationCriteria.CompanyRate = payload.CompanyRate;
            verificationCriteria.CompanyOpinion = payload.CompanyOpinion;
            verificationCriteria.ReviewComment = payload.ReviewComment;
            verificationCriteria.ReviewResult = payload.ReviewResult;
            return await verificationCriteriaRepository.UpdateAsync(verificationCriteria);
        }
        
        public async Task<ICollection<VerificationCriteria>> ApproveAllAsync(int processId)
        {
            var verificationCriterias = verificationCriteriaRepository.GetByProcessId(processId);
            if (verificationCriterias.Count == 0)
            {
                throw new BadHttpRequestException("VerificationCriteriaEmpty");
            }
            using var transaction = unitOfWork.BeginTransaction();
            foreach(var criteria in verificationCriterias)
            {
                criteria.ApprovedStatus = AppConstants.VerificationCriteriaStatus.VERIFIED;
                await verificationCriteriaRepository.UpdateAsync(criteria);
            }
            transaction.Commit();
            return verificationCriterias;
        }
    }
}
