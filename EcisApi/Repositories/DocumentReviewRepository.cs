using EcisApi.Data;
using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Repositories
{
    public interface IDocumentReviewRepository : IRepository<DocumentReview>
    {
        ICollection<DocumentReview> GetByDocumentId(int documentId);
    }

    public class DocumentReviewRepository : Repository<DocumentReview>, IDocumentReviewRepository
    {
        public DocumentReviewRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<DocumentReview> GetByDocumentId(int documentId)
        {
            return db.Set<DocumentReview>().Where(x => x.VerificationDocumentId == documentId).ToList();
        }
    }
}
