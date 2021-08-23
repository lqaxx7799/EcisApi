using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IVerificationProcessService
    {
        ICollection<VerificationProcess> GetByCompany(int companyId);
        VerificationProcess GetById(int id);
        Task<VerificationProcess> AddAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess);
    }

    public class VerificationProcessService : IVerificationProcessService
    {
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        public VerificationProcessService(
            IVerificationProcessRepository verificationProcessRepository
            )
        {
            this.verificationProcessRepository = verificationProcessRepository;
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

        public async Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess)
        {
            return await verificationProcessRepository.UpdateAsync(verificationProcess);
        }
    }
}
