﻿using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IVerificationService
    {
        Task<VerificationProcess> AddAsync(VerificationProcess verificationProcess);
        Task<VerificationProcess> UpdateAsync(VerificationProcess verificationProcess);
    }

    public class VerificationService : IVerificationService
    {
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        public VerificationService(
            IVerificationProcessRepository verificationProcessRepository
            )
        {
            this.verificationProcessRepository = verificationProcessRepository;
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
