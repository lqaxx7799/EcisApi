using EcisApi.Models;
using EcisApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IDocumentReviewService
    {
        ICollection<DocumentReview> GetByProcessId(int processId);
        ICollection<DocumentReview> GetByDocumentId(int documentId);
        DocumentReview GetById(int id);
        Task<DocumentReview> AddAsync(DocumentReview documentReview);
        Task<DocumentReview> UpdateAsync(DocumentReview documentReview);
        Task DeleteAsync(int id);
    }

    public class DocumentReviewService : IDocumentReviewService
    {
        protected readonly IDocumentReviewRepository documentReviewRepository;

        public DocumentReviewService(
            IDocumentReviewRepository documentReviewRepository
            )
        {
            this.documentReviewRepository = documentReviewRepository;
        }

        public ICollection<DocumentReview> GetByProcessId(int processId)
        {
            return documentReviewRepository.GetByProcessId(processId);
        }

        public ICollection<DocumentReview> GetByDocumentId(int documentId)
        {
            return documentReviewRepository.GetByDocumentId(documentId);
        }

        public DocumentReview GetById(int id)
        {
            return documentReviewRepository.GetById(id);
        }

        public async Task<DocumentReview> AddAsync(DocumentReview documentReview)
        {
            return await documentReviewRepository.AddAsync(documentReview);
        }

        public async Task<DocumentReview> UpdateAsync(DocumentReview documentReview)
        {
            return await documentReviewRepository.UpdateAsync(documentReview);
        }

        public async Task DeleteAsync(int id)
        {
            await documentReviewRepository.DeleteAsync(id);
        }
    }
}
