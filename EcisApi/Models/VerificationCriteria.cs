using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationCriteria : BaseModel
    {
        public int Id { get; set; }
        public string ApprovedStatus { get; set; }
        public bool CompanyRate { get; set; }
        public string CompanyOpinion { get; set; }

        public string ReviewResult { get; set; }
        public string ReviewComment { get; set; }

        public int? VerificationProcessId { get; set; }
        public int? CriteriaDetailId { get; set; }

        public virtual VerificationProcess VerificationProcess { get; set; }
        public virtual CriteriaDetail CriteriaDetail { get; set; }

        public virtual ICollection<VerificationDocument> VerificationDocuments { get; set; }
    }
}
