using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyReport : BaseModel
    {
        public int Id { get; set; }
        public string ActionTitle { get; set; }
        public string Description { get; set; }
        public DateTime AcceptedAt { get; set; }
        public DateTime HandledAt { get; set; }
        public bool IsHandled { get; set; }
        public string Status { get; set; }

        public int? VerificationProcessId { get; set; }
        public int? CompanyReportTypeId { get; set; }
        public int? TargetedCompanyId { get; set; }
        public int? CreatorCompanyId { get; set; }
        public int? AssignedAgentId { get; set; }

        public virtual VerificationProcess VerificationProcess { get; set; }
        public virtual CompanyReportType CompanyReportType { get; set; }
        public virtual Company TargetedCompany { get; set; }
        public virtual Company CreatorCompany { get; set; }
        public virtual Agent AssignedAgent { get; set; }

        [JsonIgnore]
        public virtual ICollection<CompanyReportDocument> CompanyReportDocuments { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }
    }
}
