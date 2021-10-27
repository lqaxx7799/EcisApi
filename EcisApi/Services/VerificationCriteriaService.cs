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
    }

    public class VerificationCriteriaService : IVerificationCriteriaService
    {
        protected readonly IVerificationCriteriaRepository verificationCriteriaRepository;

        public VerificationCriteriaService(
            IVerificationCriteriaRepository verificationCriteriaRepository
            )
        {
            this.verificationCriteriaRepository = verificationCriteriaRepository;
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
    }
}
