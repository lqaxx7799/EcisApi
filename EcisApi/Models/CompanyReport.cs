﻿using System;
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

        public int? VerificationProcessId { get; set; }
        public int? CompanyReportTypeId { get; set; }
        public int? TargetedCompanyId { get; set; }
        public int? CreatorCompanyId { get; set; }
        public int? AssignedAgentId { get; set; }

        public VerificationProcess VerificationProcess { get; set; }
        public CompanyReportType CompanyReportType { get; set; }
        public Company TargetedCompany { get; set; }
        public Company CreatorCompany { get; set; }
        public Agent AssignedAgent { get; set; }

        public ICollection<CompanyReportDocument> CompanyReportDocuments { get; set; }
        public ICollection<CompanyTypeModification> CompanyTypeModifications { get; set; }
    }
}