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

        public int? VerificationProcessId { get; set; }
        public int? CriteriaId { get; set; }

        public VerificationProcess VerificationProcess { get; set; }
        public Criteria Criteria { get; set; }

        public ICollection<VerificationDocument> VerificationDocuments { get; set; }
    }
}
