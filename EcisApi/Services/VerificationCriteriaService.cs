using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IVerificationCriteriaService
    {
        ICollection<VerificationCriteria> GetByProcessId(int processId);
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
    }
}
