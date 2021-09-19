using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IVerificationDocumentService
    {
        ICollection<VerificationDocument> GetByProcessId(int processId);
        Task<VerificationDocument> AddAsync(VerificationDocument verificationDocument);
        Task<ICollection<VerificationDocument>> AddBatchAsync(ICollection<VerificationDocument> verificationDocuments);
        Task<VerificationDocument> UpdateAsync(VerificationDocument verificationDocument);
        Task Delete(int id);
    }

    public class VerificationDocumentService : IVerificationDocumentService
    {
        protected readonly IVerificationDocumentRepository verificationDocumentRepository;

        public VerificationDocumentService(
            IVerificationDocumentRepository verificationDocumentRepository
            )
        {
            this.verificationDocumentRepository = verificationDocumentRepository;
        }

        public ICollection<VerificationDocument> GetByProcessId(int processId)
        {
            return verificationDocumentRepository.GetByProcessId(processId);
        }

        public async Task<VerificationDocument> AddAsync(VerificationDocument verificationDocument)
        {
            return await verificationDocumentRepository.AddAsync(verificationDocument);
        }

        public async Task<ICollection<VerificationDocument>> AddBatchAsync(ICollection<VerificationDocument> verificationDocuments)
        {
            var result = new List<VerificationDocument>();
            foreach (var document in verificationDocuments)
            {
                await verificationDocumentRepository.AddAsync(document);
                result.Add(document);
            }
            return result;
        }

        public async Task<VerificationDocument> UpdateAsync(VerificationDocument verificationDocument)
        {
            return await verificationDocumentRepository.UpdateAsync(verificationDocument);
        }

        public async Task Delete(int id)
        {
            await verificationDocumentRepository.DeleteAsync(id);
        }
    }
}
