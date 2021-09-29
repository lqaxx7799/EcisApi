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
        ICollection<DocumentReview> GetByProcessId(int processId);
        ICollection<DocumentReview> GetByDocumentId(int documentId);
    }

    public class DocumentReviewRepository : Repository<DocumentReview>, IDocumentReviewRepository
    {
        public DocumentReviewRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public ICollection<DocumentReview> GetByProcessId(int processId)
        {
            var results = from review in db.Set<DocumentReview>()
                    join document in db.Set<VerificationDocument>() on review.VerificationDocumentId equals document.Id
                    join criteria in db.Set<VerificationCriteria>() on document.VerificationCriteriaId equals criteria.Id
                    where criteria.VerificationProcessId == processId
                    select review;
            return results.ToList();

            //return db.Set<DocumentReview>().Where(x => 
            //    x.VerificationDocument.VerificationCriteria.VerificationProcessId == processId).ToList();
        }

        public ICollection<DocumentReview> GetByDocumentId(int documentId)
        {
            return db.Set<DocumentReview>().Where(x => x.VerificationDocumentId == documentId).ToList();
        }
    }
}
