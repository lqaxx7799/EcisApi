using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class CompanyTypeModification : BaseModel
    {
        public int Id { get; set; }
        
        public int? CompanyId { get; set; }
        public int? PreviousCompanyTypeId { get; set; }
        public int? UpdatedCompanyTypeId { get; set; }
        public int? ModificationTypeId { get; set; }
        public int? VerificationProcessId { get; set; }
        public int? CompanyReportId { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyType PreviousCompanyType { get; set; }
        public virtual CompanyType UpdatedCompanyType { get; set; }
        public virtual ModificationType ModificationType { get; set; }
        public virtual VerificationProcess VerificationProcess { get; set; }
        public virtual CompanyReport CompanyReport { get; set; }
    }
}
