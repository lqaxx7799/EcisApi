using EcisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class CompanyReportDTO
    {
        public string ActionTitle { get; set; }
        public string Description { get; set; }
        public int? CompanyReportTypeId { get; set; }
        public int CreatorCompanyId { get; set; }
        public int TargetedCompanyId { get; set; }
        public int? VerificationProcessId { get; set; }
        public CompanyReportDocument[] CompanyReportDocuments { get; set; }
    }
}
