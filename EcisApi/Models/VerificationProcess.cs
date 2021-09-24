using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationProcess : BaseModel
    {
        public int Id { get; set; }
        public DateTime? SubmitDeadline { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsOpenedByAgent { get; set; }
        public bool IsSubmitted { get; set; }
        public string SubmitMethod { get; set; }

        public int? CompanyTypeId { get; set; }
        public int? AssignedAgentId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Agent AssignedAgent { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual ICollection<VerificationCriteria> VerificationCriterias { get; set; }
        public virtual ICollection<CompanyReport> CompanyReports { get; set; }
        public virtual ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }

    }
}
