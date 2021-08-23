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

        public Company Company { get; set; }
        public Agent AssignedAgent { get; set; }
        public CompanyType CompanyType { get; set; }
        public ICollection<VerificationCriteria> VerificationCriterias { get; set; }
        public ICollection<CompanyReport> CompanyReports { get; set; }
        public ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }

    }
}
