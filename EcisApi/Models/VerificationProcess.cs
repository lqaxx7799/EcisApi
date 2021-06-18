using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationProcess : BaseModel
    {
        public int ID { get; set; }
        public DateTime ReviewedAt { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsOpenedByAgent { get; set; }
        public bool SubmitMethod { get; set; }

        public int CompanyTypeID { get; set; }
        public int AssignedAgentID { get; set; }
        public int CompanyID { get; set; }

        public Company Company { get; set; }
        public Agent AssignedAgent { get; set; }
        public CompanyType CompanyType { get; set; }
        public ICollection<VerificationDocument> VerificationDocuments { get; set; }
        public ICollection<CompanyAction> CompanyActions { get; set; }

    }
}
