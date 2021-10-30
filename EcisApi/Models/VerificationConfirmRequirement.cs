using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationConfirmRequirement : BaseModel
    {
        public int Id { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string ScheduledLocation { get; set; }
        public DateTime? AnnouncedAgentAt { get; set; }
        public DateTime? AnnouncedCompanyAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }

        public string AnnounceAgentDocumentContent { get; set; }
        public string AnnounceAgentDocumentUrl { get; set; }
        public string AnnounceAgentDocumentType { get; set; }
        public long AnnounceAgentDocumentSize { get; set; }
        public string AnnounceAgentDocumentName { get; set; }
        public bool IsUsingAnnounceAgentFile { get; set; }

        public string AnnounceCompanyDocumentContent { get; set; }
        public string AnnounceCompanyDocumentUrl { get; set; }
        public string AnnounceCompanyDocumentType { get; set; }
        public long AnnounceCompanyDocumentSize { get; set; }
        public string AnnounceCompanyDocumentName { get; set; }
        public bool IsUsingAnnounceCompanyFile { get; set; }

        public string ConfirmDocumentContent { get; set; }
        public string ConfirmDocumentUrl { get; set; }
        public string ConfirmDocumentType { get; set; }
        public long ConfirmDocumentSize { get; set; }
        public string ConfirmDocumentName { get; set; }
        public bool IsUsingConfirmFile { get; set; }

        public int? VerificationProcessId { get; set; }
        public int? AssignedAgentId { get; set; }
        public int? ConfirmCompanyTypeId { get; set; }
        public int? VerificationCriteriaId { get; set; }

        public virtual VerificationProcess VerificationProcess { get; set; }
        public virtual Agent AssignedAgent { get; set; }
        public virtual CompanyType ConfirmCompanyType { get; set; }
        public virtual VerificationCriteria VerificationCriteria { get; set; }
    }
}
